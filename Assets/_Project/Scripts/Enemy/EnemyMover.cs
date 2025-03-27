using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public float moveForce = 5f;  // Speed of movement
    public float moveSpeed = 3f;    // Limit on velocity to prevent overshooting
    public float standardDistance = 3f;  // Distance at which orbiting starts
    public float shortDistance = 1.5f;  // Distance at which orbiting starts
    public float longDistance = 4.5f;  // Distance at which orbiting starts
    public float orbitForce = 5f; // Force strength for orbiting
    public float attackForce = 8f; // Extra force applied when attacking
    public float attackChance = 0.1f; // 10% chance to attack per second
    public float attackCooldown = 3f; // Minimum time between attacks
    public float attackFOVThreshold = 0.7f; // Defines how "in front" the enemy has to be (-1 is behind, 1 is perfectly in front)
    public Color glowColor = Color.red; // Red glow color
    public float glowIntensity = 2f; // Intensity of the glow
    public float orbitRadius = 5f; // Default orbit multiplier

    private EnemySpawner enemySpawner;
    private Transform player;
    private Transform playerCamera;
    private ConsoleEdit console;
    private Rigidbody rb;
    private bool isOrbiting = false;
    private bool isAttacking = false;
    private float nextAttackTime = 0f;
    private Renderer enemyRenderer;
    private Material enemyMaterial;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerCamera = GameObject.Find("CenterEyeAnchor").transform;
        console = GameObject.Find("Console Output 2").GetComponent<ConsoleEdit>();
        rb = GetComponent<Rigidbody>();
        enemySpawner = GameObject.Find("Enemies").GetComponent<EnemySpawner>();
        enemyRenderer = GetComponent<Renderer>();
        enemyMaterial = enemyRenderer.material;
    }

    void FixedUpdate()
    {
        if (player == null || playerCamera == null) return;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        float orbitDistance = GetOrbitDistance(Time.time - enemySpawner.startTime);
        float tolerance = 0.5f;

        // Dynamically adjust moveSpeed based on distance to player
        moveSpeed = Mathf.Lerp(1f, 5f, distanceToPlayer / 10f); // Adjust range as needed

        // Handle movement towards or away from the player
        if (distanceToPlayer > orbitDistance + tolerance)
        {
            MoveTowardsPlayer();
        }
        else if (distanceToPlayer < orbitDistance - tolerance && !isAttacking)
        {
            MoveAwayFromPlayer();
        }

        // Handle orbiting and attacking mechanics
        if (!isAttacking && Time.time >= nextAttackTime && ShouldAttack() && Random.value < attackChance)
        {
            isAttacking = true;
            nextAttackTime = Time.time + attackCooldown;
        }

        if (isAttacking)
        {
            AttackPlayer();
        }
        else
        {
            isOrbiting = true;
            OrbitAroundPlayer();
        }

        // Dynamically clamp velocity based on moveSpeed
        Vector3 maxedVelocity = Vector3.ClampMagnitude(rb.linearVelocity, moveSpeed);
        rb.linearVelocity = maxedVelocity;
    }

    float GetOrbitDistance(float elapsedTime)
    {
        if (elapsedTime < 15) {
            return standardDistance;
        } else if (elapsedTime < 30) {
            return shortDistance;
        } else {
            return longDistance;
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        rb.AddForce(direction * moveForce, ForceMode.Acceleration);
    }

    void MoveAwayFromPlayer()
    {
        Vector3 awayFromPlayer = (transform.position - player.position).normalized;
        rb.AddForce(awayFromPlayer * moveForce, ForceMode.Acceleration);
    }

    void OrbitAroundPlayer()
    {
        Vector3 toPlayer = (player.position - transform.position).normalized;
        Vector3 orbitDirection = Vector3.Cross(toPlayer, playerCamera.up).normalized;
        Vector3 orbitForceDirection = orbitDirection * orbitForce;
        rb.AddForce(orbitForceDirection, ForceMode.Acceleration);
    }

    void AttackPlayer()
    {
        Vector3 attackDirection = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        float dynamicAttackForce = attackForce + distanceToPlayer;
        rb.AddForce(attackDirection * dynamicAttackForce, ForceMode.Impulse);
        Invoke(nameof(ResetAttack), .25f);
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    bool ShouldAttack()
    {
        Vector3 toEnemy = (transform.position - playerCamera.position).normalized;
        float dotProduct = Vector3.Dot(playerCamera.forward, toEnemy);

        return dotProduct > attackFOVThreshold;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("hitbox1"))
        {
            EnableEmission();
            Invoke(nameof(DisableEmission), 1f);
        }
    }

    void EnableEmission() {
        enemyMaterial.EnableKeyword("_EMISSION");
        enemyMaterial.SetColor("_EmissionColor", glowColor * glowIntensity);
    }

    void DisableEmission() {
        enemyMaterial.DisableKeyword("_EMISSION");
    }
}