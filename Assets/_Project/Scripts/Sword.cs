using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public MeshSlicer slicer;
    public AudioSource audioSource; // Reference to the AudioSource
    public AudioClip sliceSound; // Assign your sound in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box")) // Ensure the boxes are tagged as \"Box\"
        {
            Vector3 contactPoint = other.ClosestPoint(transform.position);
            Vector3 normal = transform.forward; // Assume slicing plane is along the sword's forward direction

            slicer.Slice(other.gameObject, contactPoint, normal);
            if (audioSource != null && sliceSound != null)
            {
            audioSource.PlayOneShot(sliceSound); // Plays the sound once
            }
        }
    }
}