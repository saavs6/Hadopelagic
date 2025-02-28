using UnityEngine;
using UnityEngine.XR;

public class DualControllerDashMovement : MonoBehaviour
{     
    public Rigidbody playerRb;
    public XRNode leftControllerNode = XRNode.LeftHand;
    public XRNode rightControllerNode = XRNode.RightHand;

    public float preDashCoefficient = 1.0f; // Smoother initial speed buildup
    public float dragCoefficient = 1.0f;    // Gradual deceleration (adjust as needed)
    public float dashSmoothTime = 0.3f;     // Time to smooth the dash speed transition

    private Vector3 startPos;
    private float startTime;

    private bool isLeftGrabbing;
    private bool isRightGrabbing;

    private bool preDash;
    private Vector3 currentVelocity = Vector3.zero;

    void Update()
    {
        bool newLeftGrabbing = IsGrabbing(leftControllerNode);
        bool newRightGrabbing = IsGrabbing(rightControllerNode);

        // Trigger the preDash when the user first grabs
        if ((!isLeftGrabbing && newLeftGrabbing) || (!isRightGrabbing && newRightGrabbing)) 
        {
            startPos = transform.position;
            startTime = Time.time;
            preDash = true;
        }

        // Perform dash when user releases the grab
        if ((isLeftGrabbing && !newLeftGrabbing) || (isRightGrabbing && !newRightGrabbing)) 
        {
            Dash();
        }

        isLeftGrabbing = newLeftGrabbing;
        isRightGrabbing = newRightGrabbing;
    }

    void FixedUpdate()
    {   
        if (preDash) 
        {
            // Smooth pre-dash acceleration
            Vector3 targetVelocity = preDashCoefficient * (transform.position - startPos).normalized;
            playerRb.linearVelocity = Vector3.SmoothDamp(playerRb.linearVelocity, targetVelocity, ref currentVelocity, dashSmoothTime);
        } 
        else if (playerRb.linearVelocity != Vector3.zero) 
        {
            // Gradual deceleration
            Vector3 dragForce = -dragCoefficient * Mathf.Pow(playerRb.linearVelocity.magnitude, 2) * playerRb.linearVelocity.normalized;
            playerRb.AddForce(dragForce, ForceMode.Force);

            // Smooth stop when the velocity is close to zero
            if (playerRb.linearVelocity.magnitude < 0.1f)
            {
                playerRb.linearVelocity = Vector3.zero;
            }
        }
    }

    void Dash() 
    {
        preDash = false;
        Vector3 endPos = transform.position;
        float duration = Time.time - startTime;

        if (duration > 0.1f) 
        {
            // Smooth dash force calculation over time
            Vector3 dashVelocity = (endPos - startPos) / duration;
            Vector3 dashForce = playerRb.mass * dashVelocity / duration;
            playerRb.AddForce(dashForce, ForceMode.Impulse);
        }
    }

    bool IsGrabbing(XRNode controllerNode)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(controllerNode);

        bool frontTriggerPressed = device.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerValue) && triggerValue;
        bool sideGripPressed = device.TryGetFeatureValue(CommonUsages.gripButton, out bool gripValue) && gripValue;

        return frontTriggerPressed && sideGripPressed;
    }
}