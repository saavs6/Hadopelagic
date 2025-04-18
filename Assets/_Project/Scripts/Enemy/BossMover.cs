using System.Diagnostics;
using System.Collections;
using Meta.WitAi;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class BossMover : BaseMover
{
    public float specialAttackCooldown = 5f;
    private float nextSpecialAttackTime = 0f;
    public float attackDistance = 0.1f;
    public Stopwatch tester = Stopwatch.StartNew();
    public bool tailWhipping = false;
    
    public GameObject rock;
    
    public float baseAttackForce = 1f;
    public float maxAttackForce = 30f;
    public float attackChance = 0.05f;
    public float attackFOVThreshold = 0.7f;
    public float attackCooldown = 3f;
    
    protected override void HandleMovement()
    {
        Vector3 x = player.position;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        float orbitDistance = LevelManager.Instance.bossDistance;
        float tolerance = 0.5f;
        float height = transform.position.y;

        float distanceRatio = Mathf.Clamp01(distanceToPlayer / 10f);
        float currentMoveSpeed = Mathf.Lerp(baseMoveSpeed, maxMoveSpeed, distanceRatio);

        bool fartherThanDistance = distanceToPlayer > orbitDistance + tolerance;
        bool closerThanDistance = distanceToPlayer < orbitDistance - tolerance;
        
        if (height < player.position.y + 2)
        {
            rb.AddForce(Vector3.up, ForceMode.Acceleration);
        }
        else if (height > player.position.y + 2)
        {
            rb.AddForce(Vector3.down, ForceMode.Acceleration);
        }
        if (fartherThanDistance)
        {
            MoveTowardsPlayer();
        }
        else if (closerThanDistance && !isAttacking)
        {
            MoveAwayFromPlayer();
        }

        if (!isAttacking) {
            OrbitAroundPlayer();
        }

        Vector3 maxedVelocity = Vector3.ClampMagnitude(rb.linearVelocity, currentMoveSpeed);
        rb.linearVelocity = maxedVelocity;
        RotateTowardsVelocity(10);
    }
    

    protected override void HandleAttack()
    {
        if (LevelManager.Instance.bossAttacking && !isAttacking)
        {
            isAttacking = true;
        }

        if (isAttacking) {
            Attack();
        }

        if (LevelManager.Instance.bossTailWhipping && !tailWhipping)
        {
            tailWhipping = true;
        }

        if (tailWhipping)
        {
            TailWhip();
        }
    }
    
    protected override void Attack()
    {
        Vector3 attackDirection = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        float distanceRatio = Mathf.Clamp01(distanceToPlayer / LevelManager.Instance.bossDistance);
        float dynamicAttackForce = Mathf.Lerp(baseAttackForce, maxAttackForce, distanceRatio);

        if (distanceRatio > 0.6f)
        {
            rb.AddForce(attackDirection * dynamicAttackForce, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(attackDirection * dynamicAttackForce, ForceMode.Impulse);
        }
        if (distanceToPlayer < 0.35f)
        {
            ResetAttack();
            health.TakeDamage(3);
        }
    }
    void ResetAttack()
    {
        LevelManager.Instance.bossAttacking = false;
        isAttacking = false;
    }

    public void TailWhip()
    {
        Vector3 towardPlayer = (transform.position - player.position).normalized; 
        GameObject NextRock = Instantiate(rock, transform.position - new Vector3(0, 1f, 0), Quaternion.identity);
        Rigidbody nrrb = NextRock.GetComponent<Rigidbody>();
        RockMover rm = NextRock.AddComponent<RockMover>();
        WaitThenDoSomething(0.5f);
        rm.whipped = true;
        ResetTailWhip();
    }
    
    IEnumerator WaitThenDoSomething(float seconds)
    {
        yield return new WaitForSeconds(seconds); // waits 2 seconds
        // Do whatever you want after waiting
    }

    void ResetTailWhip()
    {
        LevelManager.Instance.bossTailWhipping = false;
        tailWhipping = false;
    }
}