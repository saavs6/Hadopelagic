using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Ring
{
    public Transform centerlinePoint; // The center (spine) of the ring.
    public Vector3 basis1;            // First axis of the ring's plane.
    public Vector3 basis2;            // Second axis of the ring's plane.
    public float radius;              // The current offset from the centerline.
    public float initialRadius;       // The original offset from the centerline.
    public List<GameObject> points;   // The ring's points (in this case, fish group GameObjects).

    // Constructor.
    public Ring(Transform centerlinePoint, Vector3 basis1, Vector3 basis2, float radius)
    {
        this.centerlinePoint = centerlinePoint;
        this.basis1 = basis1;
        this.basis2 = basis2;
        this.radius = radius;
        this.initialRadius = radius; // Store the initial radius.
        points = new List<GameObject>();
    }

    // Repositions the points so that they are evenly distributed around the centerline.
    // Uses the originally computed basis vectors so that the ring’s plane remains fixed.
    public void RepositionPoints()
    {
        int count = points.Count;
        if (count == 0)
            return;

        float angleStep = 2 * Mathf.PI / count;
        for (int i = 0; i < count; i++)
        {
            float angle = i * angleStep;
            Vector3 offset = (Mathf.Cos(angle) * basis1 + Mathf.Sin(angle) * basis2) * radius;
            Vector3 newPosition = centerlinePoint.position + offset;
            points[i].transform.position = newPosition;
        }
    }

    // Sets a new radius for the ring and repositions the points.
    public void SetRadius(float newRadius)
    {
        radius = newRadius;
        RepositionPoints();
    }

    // Resets the ring's radius to its initial value and repositions the points.
    public void ResetRadius()
    {
        radius = initialRadius;
        RepositionPoints();
    }
}

public class RingManager : MonoBehaviour
{
    [Header("Centerline & Offsets")]
    public Transform[] centerlinePoints; // Predetermined centerline points along the shark's body.
    public float[] ringOffsets;          // Predetermined offsets for each ring.
    public float defaultOffset = 1.0f;     // Fallback offset if no entry exists in ringOffsets.

    [Header("Ring Settings")]
    public int pointsPerSection = 12;      // How many points per ring.
    public Vector3 arbitraryUp = Vector3.up; // Used for computing the cross-sectional plane.

    [Header("Parenting")]
    public Transform sharkBoss; // All ring points (and fish groups) will be parented to this transform.

    [Header("Fish Group Settings")]
    [Tooltip("Prefab for the fish group (should have FishGroupController attached)")]
    public GameObject fishGroupPrefab;

    public List<Ring> rings = new List<Ring>();

    // Store initial local positions of centerline points for resetting later.
    private Dictionary<Transform, Vector3> initialCenterlineLocalPositions = new Dictionary<Transform, Vector3>();

    void Start()
    {
        // Record the initial local positions for each centerline point.
        foreach (Transform cl in centerlinePoints)
        {
            initialCenterlineLocalPositions[cl] = cl.localPosition;
        }

        // Create a ring for each centerline point.
        for (int i = 0; i < centerlinePoints.Length; i++)
        {
            Transform currentCenter = centerlinePoints[i];

            // Determine the tangent for this centerline point.
            Vector3 tangent;
            if (i < centerlinePoints.Length - 1)
                tangent = (centerlinePoints[i + 1].position - currentCenter.position).normalized;
            else if (i > 0)
                tangent = (currentCenter.position - centerlinePoints[i - 1].position).normalized;
            else
                tangent = Vector3.forward; // Fallback for a single point.

            // Compute basis vectors for the plane perpendicular to the tangent.
            Vector3 basis1 = Vector3.Cross(tangent, arbitraryUp).normalized;
            if (basis1 == Vector3.zero)
                basis1 = Vector3.Cross(tangent, Vector3.right).normalized;
            Vector3 basis2 = Vector3.Cross(tangent, basis1).normalized;

            // Determine the offset (ring radius) for this ring.
            float ringOffset = defaultOffset;
            if (ringOffsets != null && ringOffsets.Length > i)
                ringOffset = ringOffsets[i];

            // Create a new ring with the correct orientation.
            Ring newRing = new Ring(currentCenter, basis1, basis2, ringOffset);
            rings.Add(newRing);

            // For each ring, create a fish group at each ring point.
            for (int j = 0; j < pointsPerSection; j++)
            {
                float angle = j * (2 * Mathf.PI / pointsPerSection);
                Vector3 offset = (Mathf.Cos(angle) * basis1 + Mathf.Sin(angle) * basis2) * ringOffset;
                Vector3 pointPosition = currentCenter.position + offset;

                GameObject fishGroup = null;
                if (fishGroupPrefab != null)
                {
                    fishGroup = Instantiate(fishGroupPrefab, pointPosition, Quaternion.identity);
                    // Assign the sharkBoss transform to the fish group's controller.
                    FishGroupController controller = fishGroup.GetComponent<FishGroupController>();
                    if (controller != null)
                    {
                        controller.sharkBoss = sharkBoss;
                    }
                }
                else
                {
                    // Fallback: create a small sphere if no prefab is assigned.
                    fishGroup = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    fishGroup.transform.localScale = Vector3.one * 0.1f;
                    Destroy(fishGroup.GetComponent<Collider>());
                }
                // Parent the fish group to sharkBoss.
                if (sharkBoss != null)
                {
                    fishGroup.transform.SetParent(sharkBoss);
                }
                newRing.points.Add(fishGroup);
            }
        }
    }

    // Called when a point (fish group) is deleted.
    public void PointDeleted(GameObject point)
    {
        foreach (Ring ring in rings)
        {
            if (ring.points.Contains(point))
            {
                ring.points.Remove(point);
                ring.RepositionPoints();
                Destroy(point);
                break;
            }
        }
    }

    // Called when a centerline point has moved; repositions all points in the corresponding ring.
    public void MovePoints(Transform centerline)
    {
        foreach (Ring ring in rings)
        {
            if (ring.centerlinePoint == centerline)
            {
                ring.RepositionPoints();
                break;
            }
        }
    }

    // Changes the ring distance (radius) for the ring associated with the given centerline.
    public void ChangeRingDistance(Transform centerline, float newDistance)
    {
        foreach (Ring ring in rings)
        {
            if (ring.centerlinePoint == centerline)
            {
                ring.SetRadius(newDistance);
                break;
            }
        }
    }

    // Resets the ring distance for the given centerline back to its initial offset.
    public void ResetRingDistance(Transform centerline)
    {
        foreach (Ring ring in rings)
        {
            if (ring.centerlinePoint == centerline)
            {
                ring.ResetRadius();
                break;
            }
        }
    }

    // Resets the transform of each centerline to its original local position relative to its parent.
    public void ResetCenterlinePositions()
    {
        foreach (KeyValuePair<Transform, Vector3> kvp in initialCenterlineLocalPositions)
        {
            kvp.Key.localPosition = kvp.Value;
        }
    }
}
