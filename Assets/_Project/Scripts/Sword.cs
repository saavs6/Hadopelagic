using Meta.XR.ImmersiveDebugger.UserInterface.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public MeshSlicer slicer;
    public AudioSource audioSource; // Reference to the AudioSource
    public AudioClip sliceSound; // Assign your sound in the Inspector
    public AudioClip blockSound;
    public ConsoleEdit output;
    public Boss currentBoss;

    private Vector3 position;
    private bool moving;

    void Start()
    {
        position = transform.position;
    }

    void Update()
    {
        Vector3 newPosition = transform.position;
        moving = !newPosition.Equals(position);
        position = newPosition;
    }


    private void OnTriggerEnter(Collider other)
    {

        if (!moving) return;

        if (other.CompareTag("Sliceable") || other.CompareTag("Enemy"))
        {
            Vector3 contactPoint = other.ClosestPoint(transform.position);
            Vector3 normal = transform.forward; // Assume slicing plane is along the sword's forward direction

            slicer.Slice(other.gameObject, contactPoint, normal);
            audioSource.PlayOneShot(sliceSound);

            Button btn = other.gameObject.GetComponent<Button>();
            if (btn != null) {
                btn.Action();
            }
        }
        else if (other.CompareTag("Boss"))
        {
            Vector3 contactPoint = other.ClosestPoint(transform.position);
            Vector3 normal = transform.forward;
            int result=0;
            
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
        }
    }
}