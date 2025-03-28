using UnityEngine;

public abstract class BaseMover : MonoBehaviour
{
    public float baseMoveSpeed = 3f;
    public float maxMoveSpeed = 5f;
    public float moveForce = 5f;
    public float orbitForce = 5f;
    public float rotationSpeed = 10f;
    public float minVelocityToRotate = 0.1f;

    protected Transform player;
    protected Transform playerCamera;
    protected Rigidbody rb;
    protected ConsoleEdit console;

    protected bool isAttacking;
    protected float nextAttackTime = 0f;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerCamera = GameObject.Find("CenterEyeAnchor").transform;
        rb = GetComponent<Rigidbody>();
    }

    protected void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        rb.AddForce(direction * moveForce, ForceMode.Acceleration);
    }

    protected void MoveAwayFromPlayer()
    {
        Vector3 awayFromPlayer = (transform.position - player.position).normalized;
        rb.AddForce(awayFromPlayer * moveForce, ForceMode.Acceleration);
    }

    protected void OrbitAroundPlayer()
    {
        Vector3 toPlayer = (player.position - transform.position).normalized;
        Vector3 orbitDirection = Vector3.Cross(toPlayer, playerCamera.up).normalized;
        rb.AddForce(orbitDirection * orbitForce, ForceMode.Acceleration);
    }

    protected void RotateTowardsVelocity(int addedRotation = 0)
    {
        if (rb.linearVelocity.magnitude > minVelocityToRotate)
        {
            Quaternion targetRotation = Quaternion.LookRotation(rb.linearVelocity.normalized) * Quaternion.Euler(0, 90 + addedRotation, 0);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.fixedDeltaTime
            );
        }
    }

    protected abstract void HandleMovement();
    protected abstract void HandleAttack();
    protected abstract void Attack();

    private void FixedUpdate()
    {
        if (player == null || playerCamera == null) return;

        HandleAttack();
        HandleMovement();
    }
}
