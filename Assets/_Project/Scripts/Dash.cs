using UnityEngine;
using UnityEngine.XR;

public class Dash : MonoBehaviour
{     
    public Rigidbody playerRb;
    public XRNode leftControllerNode = XRNode.LeftHand;
    public XRNode rightControllerNode = XRNode.RightHand;

    public float preDashCoefficient = 10.0f;
    public float dragCoefficient = 10.0f;

    private Vector3 startPos;
    private float startTime;

    private bool isGrabbing;

    private bool preDash;

    void Update()
    {
        bool newIsGrabbing = IsGrabbing(leftControllerNode) && IsGrabbing(rightControllerNode);

        if (!isGrabbing && newIsGrabbing) 
        {
            startPos = getPositionOnGrabs();
            startTime = Time.time;
            preDash = true;
        }

        if (isGrabbing && !newIsGrabbing) 
        {
            StartDash();
        }

        isGrabbing = newIsGrabbing;
    }

    void FixedUpdate()
    {   
        
        if (preDash) 
        {
            Vector3 deltaPos = getPositionOnGrabs() - startPos;
            if (deltaPos.magnitude > 0.1f) {
                Vector3 targetVelocity = preDashCoefficient * (getPositionOnGrabs() - startPos).normalized;
                playerRb.linearVelocity = -targetVelocity;
            }
        } 
        else if (playerRb.linearVelocity != Vector3.zero) 
        {
            Vector3 dragForce = - dragCoefficient * Mathf.Pow(playerRb.linearVelocity.magnitude, 2) * playerRb.linearVelocity.normalized;
            playerRb.AddForce(dragForce, ForceMode.Force);

            if (playerRb.linearVelocity.magnitude < 0.1f)
            {
                playerRb.linearVelocity = Vector3.zero;
            }
        }
    }

    void StartDash() 
    {
        preDash = false;
        Vector3 endPos = getPositionOnGrabs();
        float duration = Time.time - startTime;
        Vector3 deltaPos = endPos - startPos;

        if (duration > 0.1f && deltaPos.magnitude > 0.1f) 
        {
            Vector3 dashVelocity = (endPos - startPos) / duration;
            Vector3 dashForce = playerRb.mass * dashVelocity / duration;
            playerRb.AddForce(-dashForce, ForceMode.Impulse);
        }
    }

    bool IsGrabbing(XRNode controllerNode)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(controllerNode);
        bool sideGripPressed = device.TryGetFeatureValue(CommonUsages.gripButton, out bool gripValue) && gripValue;
        return sideGripPressed;
    }

    Vector3 getControllerPosition(XRNode controllerNode) {
        InputDevice device = InputDevices.GetDeviceAtXRNode(controllerNode);
        if (device.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position)) {
                return position;
        }
        return Vector3.zero;
    }

    Vector3 getPositionOnGrabs() {
        return (getControllerPosition(leftControllerNode) + getControllerPosition(rightControllerNode)) / 2;
    }
}