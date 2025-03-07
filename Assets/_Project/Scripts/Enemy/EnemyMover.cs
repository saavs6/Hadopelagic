using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public float moveSpeed = 10f;  // Speed of movement
    public float maxSpeed = 5f;    // Limit on velocity to prevent overshooting
    public float playerDistance = 5f;  // Distance at which orbiting starts
    public float orbitSpeed = 5f;  // Speed of orbiting movement
    public float orbitForceMultiplier = 10f; // Force strength for orbiting
    public float attackForce = 15f; // Extra force applied when attacking
    public float attackChance = 0.1f; // 10% chance to attack per second
    public float attackCooldown = 3f; // Minimum time between attacks
    public float attackFOVThreshold = 0.3f; // Defines how "in front" the enemy has to be (-1 is behind, 1 is perfectly in front)

    private Transform player;
    public Transform playerCamera;
    private Rigidbody rb;
    private bool isOrbiting = false;
    private bool isAttacking = false;
    private float nextAttackTime = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerCamera = GameObject.Find("CenterEyeAnchor").transform;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (player == null || playerCamera == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > playerDistance)
        {
            isOrbiting = false;
            isAttacking = false;
            MoveTowardsPlayer();
        }
        else
        {
            if (!isAttacking && Time.time >= nextAttackTime && ShouldAttack())
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
        }

        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        rb.AddForce(direction * moveSpeed, ForceMode.Acceleration);
    }

    void OrbitAroundPlayer()
    {
        Vector3 toPlayer = (player.position - transform.position).normalized;
        Vector3 orbitDirection = Vector3.Cross(toPlayer, Vector3.up).normalized;

        rb.AddForce(orbitDirection * orbitSpeed * orbitForceMultiplier, ForceMode.Acceleration);
    }

    void AttackPlayer()
    {
        Vector3 attackDirection = (player.position - transform.position).normalized;
        rb.AddForce(attackDirection * attackForce, ForceMode.Impulse);

        Invoke(nameof(ResetAttack), 0.5f);
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
            // Attack reaction logic here
        }
    }
}