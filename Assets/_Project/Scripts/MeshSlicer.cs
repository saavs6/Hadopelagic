using UnityEngine;
using EzySlice; // Import the EzySlice namespace

public class MeshSlicer : MonoBehaviour
{
    public Material cutMaterial; // Material to apply to the cut faces
    public float separationForce = 2f; // Additional force to apply to separate the pieces
    public float despawnTime = 5f; // Time in seconds before the cut pieces are destroyed

    public void Slice(GameObject targetObject, Vector3 planePoint, Vector3 planeNormal)
    {
        // Ensure the target object has a MeshFilter and MeshRenderer
        MeshFilter meshFilter = targetObject.GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = targetObject.GetComponent<MeshRenderer>();
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

        // Temporarily disable the Rigidbody to prevent collision response
        if (originalRigidbody != null)
        {
            originalRigidbody.isKinematic = true;
        }

        // Create the slicing plane
        EzySlice.Plane slicingPlane = new EzySlice.Plane(planeNormal, planePoint);

        // Perform the slice using EzySlice
        SlicedHull slicedHull = targetObject.Slice(planePoint, planeNormal, cutMaterial);

        if (slicedHull != null)
        {
            // Create the upper mesh object
            GameObject upperHull = CreateHullObject(slicedHull.upperHull, targetObject, "Upper Hull", originalVelocity, originalAngularVelocity, planeNormal);
            // Create the lower mesh object
            GameObject lowerHull = CreateHullObject(slicedHull.lowerHull, targetObject, "Lower Hull", originalVelocity, originalAngularVelocity, -planeNormal);

            // Optionally, destroy the original object
            Destroy(targetObject);
        }
        else
        {
            Debug.LogError("Slicing failed. Ensure the object has a valid mesh.");
        }
    }

    private GameObject CreateHullObject(
        Mesh hullMesh, 
        GameObject originalObject, 
        string name, 
        Vector3 inheritedVelocity, 
        Vector3 inheritedAngularVelocity, 
        Vector3 separationDirection)
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
        
        //change the layer to whatever it was in the previous object
        hullObject.layer = originalObject.layer;

        // Apply the cutMaterial if it's available
        if (cutMaterial != null)
        {
            meshRenderer.material = cutMaterial;
        }

        // Add a Rigidbody to the new piece
        Rigidbody hullRigidbody = hullObject.AddComponent<Rigidbody>();
        hullRigidbody.linearVelocity = inheritedVelocity; // Apply inherited velocity
        hullRigidbody.angularVelocity = inheritedAngularVelocity; // Apply inherited angular velocity

        // Apply a separation force
        hullRigidbody.AddForce(separationDirection.normalized * separationForce, ForceMode.Impulse);

        // Add a collider for physics interactions
        MeshCollider hullCollider = hullObject.AddComponent<MeshCollider>();
        hullCollider.sharedMesh = hullMesh;
        hullCollider.convex = true; // Ensure the collider is convex for Rigidbody compatibility
        
        Destroy(hullObject, despawnTime);
        return hullObject;
    }
}
