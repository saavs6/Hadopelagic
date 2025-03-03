using UnityEngine;

public class FishBehavior : MonoBehaviour
{
    [Tooltip("Assign the Shark GameObject's transform here.")]
    public Transform sharkTransform;

    [Tooltip("Minimum movement speed to consider the fish as 'moving' on its own.")]
    public float velocityThreshold = 0.1f;
    
    [Tooltip("How quickly the fish turns toward its target direction.")]
    public float turnSpeed = 2.0f;

    private Rigidbody rb;
    private Vector3 lastPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(rb == null)
        {
            Debug.LogWarning("FishBehavior expects a Rigidbody component for velocity tracking.");
        }
        lastPosition = transform.position;
    }

    void Update()
    {
        // Determine movement direction.
        Vector3 moveDirection = Vector3.zero;
        if (rb != null)
        {
            moveDirection = rb.linearVelocity;
        }
        else
        {
            // Fallback: approximate velocity from position change.
            moveDirection = (transform.position - lastPosition) / Time.deltaTime;
        }
        lastPosition = transform.position;
        
        // If the fish is moving significantly, face that direction;
        // otherwise, face toward the shark.
        Vector3 targetDirection = (moveDirection.magnitude > velocityThreshold) ?
            moveDirection.normalized :
            ((sharkTransform != null) ? (sharkTransform.position - transform.position).normalized : transform.forward);
        
        if(targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }
}
