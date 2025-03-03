using System.Collections;
using UnityEngine;

public class BossTesting : MonoBehaviour
{
    public RingManager ringManager; // Reference to our RingManager instance.

    void Start()
    {
        StartCoroutine(TestSequence());
    }

    IEnumerator TestSequence()
    {
        // Wait 3 seconds and then test deleting a point.
        yield return new WaitForSeconds(3f);
        Debug.Log("Testing PointDeleted: Deleting the first point of the first ring.");
        if (ringManager != null && ringManager.rings != null && ringManager.rings.Count > 0 &&
            ringManager.rings[0].points.Count > 0)
        {
            GameObject pointToDelete = ringManager.rings[0].points[0];
            ringManager.PointDeleted(pointToDelete);
        }
        else
        {
            Debug.LogWarning("No rings or points found for deletion test.");
        }

        // Wait another 3 seconds and then test moving the centerline.
        yield return new WaitForSeconds(3f);
        Debug.Log("Testing MovePoints: Moving the first centerline point upward by 1 unit.");
        if (ringManager != null && ringManager.rings != null && ringManager.rings.Count > 0)
        {
            // Simulate moving the centerline by shifting it upward.
            Transform centerline = ringManager.rings[0].centerlinePoint;
            centerline.position += Vector3.up; // Move upward by 1 unit.
            ringManager.MovePoints(centerline);
        }
        else
        {
            Debug.LogWarning("No rings found for move test.");
        }

        // Wait another 3 seconds and then test changing the ring distance.
        yield return new WaitForSeconds(3f);
        Debug.Log("Testing ChangeRingDistance: Changing the distance for the first ring to 2.0.");
        if (ringManager != null && ringManager.rings != null && ringManager.rings.Count > 0)
        {
            Transform centerline = ringManager.rings[0].centerlinePoint;
            ringManager.ChangeRingDistance(centerline, 2.0f);
        }
        else
        {
            Debug.LogWarning("No rings found for distance change test.");
        }

        // Wait another 3 seconds and then test resetting the ring distance.
        yield return new WaitForSeconds(3f);
        Debug.Log("Testing ResetRingDistance: Resetting the distance for the first ring to its initial value.");
        if (ringManager != null && ringManager.rings != null && ringManager.rings.Count > 0)
        {
            Transform centerline = ringManager.rings[0].centerlinePoint;
            ringManager.ResetRingDistance(centerline);
        }
        else
        {
            Debug.LogWarning("No rings found for reset distance test.");
        }
    }
}
