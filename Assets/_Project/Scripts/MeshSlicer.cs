using UnityEngine;
using EzySlice; // Import the EzySlice namespace

public class MeshSlicer : MonoBehaviour
{
    public float despawnTime = 5f; // Time in seconds before the cut pieces are destroyed
    public float drag = 1f; // Drag to gradually stop the pieces
    public float angularDrag = 0.5f; // Angular drag to gradually stop rotation
    public float separationForce = 2f; // Force to push the pieces apart

    public void Slice(GameObject targetObject, Vector3 planePoint, Vector3 planeNormal)
    {
        // Ensure the target object has a MeshFilter and MeshRenderer
        MeshFilter meshFilter = targetObject.GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = targetObject.GetComponent<MeshRenderer>();
        Material cutMaterial = targetObject.GetComponent<Material>();
        Rigidbody originalRigidbody = targetObject.GetComponent<Rigidbody>();

        if (meshFilter == null || meshRenderer == null)
        {
            Debug.LogError("Target object must have a MeshFilter and MeshRenderer.");
            return;
        }

        if (meshFilter.mesh == null)
        {
            Debug.LogError("Target object has no valid mesh to slice.");
            return;
        }

        // Capture the original velocity and angular velocity before slicing
        Vector3 originalVelocity = originalRigidbody != null ? originalRigidbody.linearVelocity : Vector3.zero;
        Vector3 originalAngularVelocity = originalRigidbody != null ? originalRigidbody.angularVelocity : Vector3.zero;

        // Create the slicing plane
        EzySlice.Plane slicingPlane = new EzySlice.Plane(planeNormal, planePoint);

        // Perform the slice using EzySlice
        SlicedHull slicedHull = targetObject.Slice(planePoint, planeNormal, cutMaterial);

        if (slicedHull != null)
        {
            // Create the upper and lower hull objects with separation forces
            CreateHullObject(slicedHull.upperHull, cutMaterial, targetObject, "Upper Hull", originalVelocity, originalAngularVelocity, planeNormal);
            CreateHullObject(slicedHull.lowerHull, cutMaterial, targetObject, "Lower Hull", originalVelocity, originalAngularVelocity, -planeNormal);

            // Destroy the original object
            Destroy(targetObject);
        }
        else
        {
            Debug.LogError("Slicing failed. Ensure the object has a valid mesh.");
        }
    }

    private GameObject CreateHullObject(
        Mesh hullMesh,
        Material cutMaterial,
        GameObject originalObject,
        string name,
        Vector3 inheritedVelocity,
        Vector3 inheritedAngularVelocity,
        Vector3 separationDirection
    )
    {
        if (hullMesh == null)
            return null;

        // Create a new GameObject for the hull
        GameObject hullObject = new GameObject(name);
        hullObject.transform.position = originalObject.transform.position;
        hullObject.transform.rotation = originalObject.transform.rotation;
        hullObject.transform.localScale = originalObject.transform.localScale;

        // Add a MeshFilter and MeshRenderer
        MeshFilter meshFilter = hullObject.AddComponent<MeshFilter>();
        meshFilter.mesh = hullMesh;

        MeshRenderer meshRenderer = hullObject.AddComponent<MeshRenderer>();
        meshRenderer.materials = originalObject.GetComponent<MeshRenderer>().materials;

        // Apply the cutMaterial if it's available
        if (cutMaterial != null)
        {
            meshRenderer.material = cutMaterial;
        }

        // Add a Rigidbody to the new piece
        Rigidbody hullRigidbody = hullObject.AddComponent<Rigidbody>();
        hullRigidbody.linearVelocity = inheritedVelocity; // Apply inherited velocity
        hullRigidbody.angularVelocity = inheritedAngularVelocity; // Apply inherited angular velocity
        hullRigidbody.linearDamping = drag; // Apply drag to gradually stop the piece
        hullRigidbody.angularDamping = angularDrag; // Apply angular drag to gradually stop rotation

        // Apply a separation force to push the pieces apart
        hullRigidbody.AddForce(separationDirection.normalized * separationForce, ForceMode.Impulse);

        // Add a collider for physics interactions
        MeshCollider hullCollider = hullObject.AddComponent<MeshCollider>();
        hullCollider.sharedMesh = hullMesh;
        hullCollider.convex = true; // Ensure the collider is convex for Rigidbody compatibility

        // Destroy the hull object after a set time
        Destroy(hullObject, despawnTime);

        return hullObject;
    }
}
