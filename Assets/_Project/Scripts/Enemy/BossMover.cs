using System.Diagnostics;
using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class BossMover : BaseMover
{
    public float specialAttackCooldown = 5f;
    private float nextSpecialAttackTime = 0f;
    public float attackDistance = 0.1f;
    public Stopwatch tester = Stopwatch.StartNew();
    public bool tailWhipping = false;
    public bool charging = false;
    
    public GameObject rock;
    public bool spawned = false;
    
    public float baseAttackForce = 1f;
    public float maxAttackForce = 30f;
    public float attackChance = 0.05f;
    public float attackFOVThreshold = 0.7f;
    public float attackCooldown = 3f;

    public Vector3 chargeDirection;
    public float initDistance;
    public bool trigger = false;
    public Renderer renderer;
    public bool damaged = false;
    
    private Material ogMaterial;
    public Material newMaterial;

    public int polyCount = 3;

    public AudioSource audioSource;
    public AudioClip whipSound;
    public AudioClip blockSound;
    
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
            charging = false;
            polyCount = LevelManager.Instance.poly;
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

        if (LevelManager.Instance.bossCharging && !charging)
        {
            charging = true;
            chargeDirection = (player.position - transform.position);
            initDistance = Vector3.Distance(transform.position, player.position);
            renderer = GetComponent<Renderer>();
            ogMaterial = renderer.material;
        }

        if (charging)
        {
            Charge();
        }
    }
    
    protected override void Attack()
    {
        Vector3 attackDirection = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        float distanceRatio = Mathf.Clamp01(distanceToPlayer / LevelManager.Instance.bossDistance);
        float dynamicAttackForce = Mathf.Lerp(baseAttackForce, maxAttackForce, distanceRatio);

        if (!spawned)
        {
            spawned = true;
            PS.SpawnPolygon(polyCount);
        }

        if (distanceRatio > 0.4f)
        {
            rb.AddForce(attackDirection * dynamicAttackForce, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(attackDirection * dynamicAttackForce, ForceMode.Impulse);
        }
        if (distanceToPlayer < 0.225f)
        {
            if (!PS.Success())  
            {
                health.TakeDamage(3);
            }
            else
            {
                audioSource.PlayOneShot(blockSound);
            }
            ResetAttack();
            
        }
    }
    public void TailWhip()
    {
        Vector3 towardPlayer = (transform.position - player.position).normalized; 
        GameObject NextRock = Instantiate(rock, transform.position - new Vector3(0, 1f, 0), Quaternion.identity);
        Rigidbody nrrb = NextRock.GetComponent<Rigidbody>();
        RockMover rm = NextRock.AddComponent<RockMover>();
        WaitThenDoSomething(0.5f);
        audioSource.PlayOneShot(whipSound);
        rm.whipped = true;
        ResetTailWhip();
    }

    public void Charge()
    {
        Vector3 attackDirection = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (renderer != null)
        {
        //    renderer.material = newMaterial;
        }
        
        if (distanceToPlayer < (initDistance*0.7f))
        {
            trigger = true;
        }
        
        if (!damaged && distanceToPlayer < 2f)
        {
            damaged = true;
            health.TakeDamage(4);
        } 
        else if (trigger && distanceToPlayer > initDistance*0.75f)
        {
            ResetCharge();
        } 
        else
        {
            rb.AddForce(chargeDirection.normalized * 8.5f, ForceMode.Impulse);
        }
    }
    
    IEnumerator WaitThenDoSomething(float seconds)
    {
        yield return new WaitForSeconds(seconds); // waits 2 seconds
        // Do whatever you want after waiting
    }

    
    void ResetAttack()
    {
        LevelManager.Instance.bossAttacking = false;
        isAttacking = false;
        spawned = false;
        PS.DestroyAll();
    }
    void ResetTailWhip()
    {
        LevelManager.Instance.bossTailWhipping = false;
        tailWhipping = false;
    }

    void ResetCharge()
    {
        LevelManager.Instance.bossCharging = false;
        charging = false;
        trigger = false;
        damaged = false;
        //renderer.material = ogMaterial;
    }
}