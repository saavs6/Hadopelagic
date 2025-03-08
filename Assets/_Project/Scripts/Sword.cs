using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public MeshSlicer slicer;

    private void OnTriggerEnter(Collider other)
    {
<<<<<<< Updated upstream
        if (other.CompareTag("Box")) // Ensure the boxes are tagged as \"Box\"
=======
        if (other.CompareTag("Enemy") || other.CompareTag("Sliceable")) // Ensure the boxes are tagged as \"Box\"
>>>>>>> Stashed changes
        {
            Vector3 contactPoint = other.ClosestPoint(transform.position);
            Vector3 normal = transform.forward; // Assume slicing plane is along the sword's forward direction

            slicer.Slice(other.gameObject, contactPoint, normal);
        }
    }
}