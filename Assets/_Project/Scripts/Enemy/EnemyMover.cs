using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public float moveForce = 5f;  // Speed of movement
    public float moveSpeed = 3f;    // Limit on velocity to prevent overshooting
    public float playerDistance = 3f;  // Distance at which orbiting starts
    public float orbitForce = 5f; // Force strength for orbiting
    public float attackForce = 8f; // Extra force applied when attacking
    public float attackChance = 0.1f; // 10% chance to attack per second
    public float attackCooldown = 3f; // Minimum time between attacks
    public float attackFOVThreshold = 0.3f; // Defines how "in front" the enemy has to be (-1 is behind, 1 is perfectly in front)

    private Transform player;
    private Transform playerCamera;
    private ConsoleEdit console;
    private Rigidbody rb;
    private bool isOrbiting = false;
    private bool isAttacking = false;
    private float nextAttackTime = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerCamera = GameObject.Find("CenterEyeAnchor").transform;
        console = GameObject.Find("Console Output 2").GetComponent<ConsoleEdit>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (player == null || playerCamera == null) return;
        console.UpdateText("No Hit");

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > playerDistance)
        {
            isOrbiting = false;
            isAttacking = false;
            MoveTowardsPlayer();
        }
        else
        {
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
        }

        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, moveSpeed);
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        rb.AddForce(direction * moveForce, ForceMode.Acceleration);
    }

    void OrbitAroundPlayer()
    {
        Vector3 toPlayer = (player.position - transform.position).normalized;
        Vector3 orbitDirection = Vector3.Cross(toPlayer, playerCamera.up).normalized;
        rb.AddForce(orbitDirection * orbitForce, ForceMode.Acceleration);
    }

    void AttackPlayer()
    {
        Vector3 attackDirection = (player.position - transform.position).normalized;
        rb.AddForce(attackDirection * attackForce, ForceMode.Impulse);

        Invoke(nameof(ResetAttack), 0.25f);
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
            console.UpdateText("Hit");
        }
    }
}