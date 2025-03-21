using UnityEngine;

public class Boss : MonoBehaviour
{
    public int bossHealth = 100;
    public ConsoleEdit output;
    public bool undamageable = false;
    
    public Transform centerPoint; // The point to circle around
    public float radius = 5f;     // Radius of the circle
    public float speed = 2f;      // Speed of the movement
    private float angle = 0f;     // Angle for calculating position
    private Vector3 previousPosition;
    public Material unDMaterial; // Assign this in the Inspector
    public Material damageMaterial;
    private Renderer objRenderer;
    
    void Start()
    {
        // Record the initial position
        previousPosition = transform.position;
        output.UpdateText("Health: " + bossHealth);
        objRenderer = GetComponent<Renderer>(); // Get the Renderer component
    }
    
    void Update()
    {
        // Increment the angle over time
        angle += speed * Time.deltaTime;
        if (objRenderer != null && unDMaterial != null && damageMaterial != null)
        {
            if (undamageable)
            {
                objRenderer.material = unDMaterial; // Change the material  
            }
            else
            {
                objRenderer.material = damageMaterial; // Change the material  
            }
        }

        // Calculate the new position using Sin and Cos
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        // Update the position of the object
        Vector3 newPosition = new Vector3(
            centerPoint.position.x + x, 
            transform.position.y,          // Keep the current Y position
            centerPoint.position.z + z
        );

        transform.position = newPosition;

        // Rotate to face the movement direction
        Vector3 direction = newPosition - previousPosition;
        if (direction != Vector3.zero) // Prevent NaN errors
        {
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.Rotate(0f, 90f, 0f);
        }

        // Update the previous position
        previousPosition = newPosition;
    }
    
    public int getBossHealth()
    {
        return bossHealth;
    }
    
    public int Damage()
    {
        if (undamageable)
        {
            return 2;
        }
        
        if (bossHealth > 0) {
            bossHealth--;
            output.UpdateText("Health: " + bossHealth);
            return 1;
        }

        return -1;
    }
}
