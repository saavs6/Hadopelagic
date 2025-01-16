using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperTorsoPush : MonoBehaviour
{
    [Header("Dash Settings")]
    public float dashSpeed = 10f; // Speed of the dash
    public float dashDuration = 0.2f; // Duration of the dash (in seconds)

    [Header("References")]
    public Rigidbody ovrCameraRigRigidbody; // Reference to the OVR Camera Rig's Rigidbody
    public Transform centerEyeAnchor; // Reference to the CenterEyeAnchor

    private bool isDashing = false;
    private Vector3 dashDirection;

    private void Start()
    {
        // Validate that Rigidbody is assigned
        if (ovrCameraRigRigidbody == null)
        {
            Debug.LogError("Rigidbody not assigned. Please assign the Rigidbody of the OVR Camera Rig in the Inspector.");
        }

        // Automatically find the CenterEyeAnchor if not set
        if (centerEyeAnchor == null)
        {
            centerEyeAnchor = GameObject.Find("CenterEyeAnchor")?.transform;
            if (centerEyeAnchor == null)
            {
                Debug.LogError("CenterEyeAnchor not found. Please assign it manually in the Inspector.");
            }
        }
    }

    private void yes()//OnTriggerEnter(Collider other)
    {
        // Check if the object colliding is a hand (based on its tag or layer)
        if (//other.CompareTag("Hand") && 
        !isDashing)
        {
            Debug.Log("Dash triggered by hand collision.");
            StartDash();
        }
    }

    private void StartDash()
    {
        if (centerEyeAnchor == null || ovrCameraRigRigidbody == null)
        {
            Debug.LogError("Missing required components for dash. Cannot start dash.");
            return;
        }

        isDashing = true;

        // Calculate the direction based on the player's head (CenterEyeAnchor) forward direction
        dashDirection = centerEyeAnchor.forward;

        // Start the dash coroutine
        StartCoroutine(DashCoroutine());
    }

    private System.Collections.IEnumerator DashCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            // Move the player in the dash direction
            ovrCameraRigRigidbody.velocity = dashDirection * dashSpeed;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Stop the dash
        ovrCameraRigRigidbody.velocity = Vector3.zero;
        isDashing = false;
    }
}
