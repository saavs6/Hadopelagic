using UnityEngine;

public class BossMover : BaseMover
{
    public float specialAttackCooldown = 5f;
    private float nextSpecialAttackTime = 0f;

    protected override void HandleMovement()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        float orbitDistance = LevelManager.GetBossWaveOneOrbitDistance();
        float tolerance = 0.5f;

        float distanceRatio = Mathf.Clamp01(distanceToPlayer / 10f);
        float currentMoveSpeed = Mathf.Lerp(baseMoveSpeed, maxMoveSpeed, distanceRatio);

        bool fartherThanDistance = distanceToPlayer > orbitDistance + tolerance;
        bool closerThanDistance = distanceToPlayer < orbitDistance - tolerance;

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
        // if (Time.time >= nextSpecialAttackTime)
        // {
        //     nextSpecialAttackTime = Time.time + specialAttackCooldown;
        //     PerformSpecialAttack();
        // }
    }

    protected override void Attack()
    {
        
    }

    private void PerformSpecialAttack()
    {
        Debug.Log("Boss performs a special attack!");
        // Implement boss-specific attack logic here
    }
}
