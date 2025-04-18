using UnityEngine;

public class MinionMover : BaseMover
{
    public float baseAttackForce = 7.5f;
    public float maxAttackForce = 20f;
    public float attackChance = 0.05f;
    public float attackFOVThreshold = 0.7f;
    public float attackCooldown = 3f;
    
    public bool isSwarming = false;
    public bool active = false;

    public float minDistanceToPlayer = .25f;
    
    public Color glowColor = Color.red;
    public float glowIntensity = 2f;

    private Renderer enemyRenderer;
    private Material enemyMaterial;

    protected override void Start()
    {
        base.Start();
        enemyRenderer = GetComponent<Renderer>();
        enemyMaterial = enemyRenderer.material;
    }

    protected override void HandleMovement()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        float orbitDistance = LevelManager.Instance.minionDistance;
        float tolerance = 0.5f;

        float distanceRatio = Mathf.Clamp01(distanceToPlayer / 10f);
        float currentMoveSpeed = Mathf.Lerp(baseMoveSpeed, maxMoveSpeed, distanceRatio);

        if (distanceToPlayer > orbitDistance + tolerance)
        {
            MoveTowardsPlayer();
        }
        else if (distanceToPlayer < orbitDistance - tolerance && !isAttacking)
        {
            MoveAwayFromPlayer();
        }

        if (!isAttacking)
        {
            OrbitAroundPlayer();
        }

        Vector3 maxedVelocity = Vector3.ClampMagnitude(rb.linearVelocity, currentMoveSpeed);
        rb.linearVelocity = maxedVelocity;
        RotateTowardsVelocity();
    }

    protected override void HandleAttack()
    {
        if (LevelManager.Instance.swarm)
        {
            isSwarming = true;
        }
        else
        {
            isSwarming = false;
            active = true;
        }
        if (!isAttacking && Time.time >= nextAttackTime && ShouldAttack() && Random.value < attackChance)
        {
            isAttacking = true;
            nextAttackTime = Time.time + attackCooldown;
        }

        if (isAttacking) {
            Attack();
        }

        if (isSwarming && active)
        {
            SwarmAttack();
        }
    }

    protected override void Attack()
    {
        Vector3 attackDirection = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        float distanceRatio = Mathf.Clamp01(distanceToPlayer / 10f);
        float dynamicAttackForce = Mathf.Lerp(baseAttackForce, maxAttackForce, distanceRatio);

        rb.AddForce(attackDirection * dynamicAttackForce, ForceMode.Impulse);
        Invoke(nameof(ResetAttack), .25f);
    }

    public void SwarmAttack()
    {
        Vector3 attackDirection = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        float distanceRatio = Mathf.Clamp01(distanceToPlayer / 10f);
        float dynamicAttackForce = Mathf.Lerp(baseAttackForce, maxAttackForce, distanceRatio);

        rb.AddForce(10 * attackDirection * dynamicAttackForce, ForceMode.Acceleration);
        if (distanceToPlayer < 0.3f)
        {
            ResetSwarm();
        }
    }

    bool ShouldAttack()
    {
        Vector3 toEnemy = (transform.position - playerCamera.position).normalized;
        float dotProduct = Vector3.Dot(playerCamera.forward, toEnemy);

        return dotProduct > attackFOVThreshold;
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    void ResetSwarm()
    {
        isSwarming = false;
        active = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            EnableEmission();
            Invoke(nameof(DisableEmission), 1f);

            Vector3 randomDirection;
            do
            {
                randomDirection = Random.onUnitSphere.normalized;
            } while (Vector3.Dot(randomDirection, rb.linearVelocity.normalized) < -0.5f ||
                     Vector3.Dot(randomDirection, rb.linearVelocity.normalized) > 0.866f);

            rb.AddForce(randomDirection * moveForce, ForceMode.Impulse);
        }
    }

    private void EnableEmission()
    {
        enemyMaterial.EnableKeyword("_EMISSION");
        enemyMaterial.SetColor("_EmissionColor", glowColor * glowIntensity);
    }

    private void DisableEmission()
    {
        enemyMaterial.DisableKeyword("_EMISSION");
    }
}