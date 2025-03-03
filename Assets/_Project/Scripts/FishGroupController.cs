using System.Collections.Generic;
using UnityEngine;

public class FishGroupController : MonoBehaviour
{
    [Header("Fish Prefab & Initial Settings")]
    [Tooltip("Prefab for the individual fish (should have FishBehavior attached).")]
    public GameObject fishPrefab;
    public int initialFishCount = 10;
    
    [Tooltip("Radius of the spherical formation for the fish.")]
    public float formationRadius = 1.0f;
    
    [Tooltip("Speed at which the fish group moves to follow its target.")]
    public float moveSpeed = 2.0f;

    [Header("Target Settings")]
    [Tooltip("The target position that this fish group should follow.")]
    public Vector3 targetPosition;
    public Transform sharkBoss; //shark's transform
    private List<GameObject> fishList = new List<GameObject>();

    void Start()
    {
        // Initially, set target to current position.
        targetPosition = transform.position;

        // Create initial fish.
        for (int i = 0; i < initialFishCount; i++)
        {
            GameObject fish = Instantiate(fishPrefab, transform);
            fish.GetComponent<FishBehavior>().sharkTransform = sharkBoss;
            fishList.Add(fish);
        }
        ArrangeFishInSphere();
    }

    void Update()
    {
        // Smoothly move the group toward the target position.
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Rearranges the fish in a spherical formation using a Fibonacci sphere algorithm.
    /// The formation is centered at the group's local origin.
    /// </summary>
    void ArrangeFishInSphere()
    {
        int n = fishList.Count;
        if(n == 0)
            return;
        
        float offset = 2f / n;
        float increment = Mathf.PI * (3f - Mathf.Sqrt(5f)); // Golden angle in radians
        
        for (int i = 0; i < n; i++)
        {
            float y = ((i * offset) - 1f) + (offset / 2f);
            float r = Mathf.Sqrt(1 - y * y);
            float phi = i * increment;
            float x = Mathf.Cos(phi) * r;
            float z = Mathf.Sin(phi) * r;
            Vector3 pos = new Vector3(x, y, z) * formationRadius;
            fishList[i].transform.localPosition = pos;
        }
    }

    /// <summary>
    /// Updates the target position for the fish group. Typically called externally when the associated ring point moves. i.e. every frame
    /// </summary>
    /// <param name="newTarget">New world-space target position.</param>
    public void SetTargetPosition(Vector3 newTarget)
    {
        targetPosition = newTarget;
    }

    /// <summary>
    /// Instantiates a new fish, adds it to the group, and rearranges the formation.
    /// </summary>
    public void AddFish()
    {
        GameObject fish = Instantiate(fishPrefab, transform);
        fishList.Add(fish);
        ArrangeFishInSphere();
    }

    /// <summary>
    /// Removes the last fish in the group (if any), destroys it, and rearranges the formation.
    /// </summary>
    public void SubtractFish()
    {
        if(fishList.Count > 0)
        {
            GameObject fish = fishList[fishList.Count - 1];
            fishList.RemoveAt(fishList.Count - 1);
            Destroy(fish);
            ArrangeFishInSphere();
        }
    }
}
