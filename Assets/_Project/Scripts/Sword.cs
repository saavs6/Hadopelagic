using UnityEngine;

public class Sword : MonoBehaviour
{
    public MeshSlicer slicer;
    public AudioSource audioSource; // Reference to the AudioSource
    public AudioClip sliceSound; // Assign your sound in the Inspector
    public AudioClip blockSound;
    public ConsoleEdit output;
    private int n = 0;
    public Boss currentBoss;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Sliceable")) // Ensure the boxes are tagged as \"Box\"
        {
            Vector3 contactPoint = other.ClosestPoint(transform.position);
            Vector3 normal = transform.forward; // Assume slicing plane is along the sword's forward direction

            slicer.Slice(other.gameObject, contactPoint, normal);
            if (audioSource != null && sliceSound != null)
            {
            audioSource.PlayOneShot(sliceSound); // Plays the sound once
            }
        }
        else if (other.CompareTag("Boss"))
        {
            Vector3 contactPoint = other.ClosestPoint(transform.position);
            Vector3 normal = transform.forward;
            int result=0;
            n += 1;
            
            if (currentBoss != null)
            {
                if (currentBoss.getBossHealth()==0)
                {
                    slicer.Slice(other.gameObject, contactPoint, normal);
                }
                else
                {
                    result = currentBoss.Damage();
                }
            }
            
            if (audioSource != null && sliceSound != null)
            {
                if (result == 1){
                    audioSource.PlayOneShot(sliceSound);
                }

                if (result == 2)
                {
                    audioSource.PlayOneShot(blockSound);
                }
            }

            if (output != null)
            {
                output.UpdateText("Number of times hit:" + n);
            }
        }
    }
}