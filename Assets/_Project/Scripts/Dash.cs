using UnityEngine;
using UnityEngine.XR;
using TMPro;

public class DualControllerDashMovement : MonoBehaviour
{
    public Transform player; // The player's transform (XR Rig)
    public XRNode leftControllerNode = XRNode.LeftHand;
    public XRNode rightControllerNode = XRNode.RightHand;
    public float movementMultiplier = 2.0f; // Adjust for movement scaling
    public float smoothingFactor = 0.1f; // Smoothing movement
    public ConsoleEdit output;


    private bool isLeftDashing = false;
    private bool isRightDashing = false;

    private Vector3 initialLeftPosition;
    private Vector3 initialRightPosition;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        output.UpdateText("Console Output: Dash Assigned!");
    }

    void Update()
    {
        if (player == null) return;

        bool leftGrabbing = IsGrabbing(leftControllerNode);
        bool rightGrabbing = IsGrabbing(rightControllerNode);

        InputDevice leftDevice = InputDevices.GetDeviceAtXRNode(leftControllerNode);
        InputDevice rightDevice = InputDevices.GetDeviceAtXRNode(rightControllerNode);

        Vector3 leftPosition = Vector3.zero, rightPosition = Vector3.zero;
        bool leftHasPosition = leftDevice.TryGetFeatureValue(CommonUsages.devicePosition, out leftPosition);
        bool rightHasPosition = rightDevice.TryGetFeatureValue(CommonUsages.devicePosition, out rightPosition);

        if (leftGrabbing && !isLeftDashing && leftHasPosition)
        {
            initialLeftPosition = leftPosition;
            isLeftDashing = true;
        }
        else if (!leftGrabbing && isLeftDashing)
        {
            isLeftDashing = false;
        }

        if (rightGrabbing && !isRightDashing && rightHasPosition)
        {
            initialRightPosition = rightPosition;
            isRightDashing = true;
        }
        else if (!rightGrabbing && isRightDashing)
        {
            isRightDashing = false;
        }

        if (isLeftDashing || isRightDashing)
        {
            output.UpdateText("Dash Attempted!");
            Vector3 displacement = Vector3.zero;

            if (isLeftDashing && leftHasPosition)
                displacement += initialLeftPosition - leftPosition;

            if (isRightDashing && rightHasPosition)
                displacement += initialRightPosition - rightPosition;

            Vector3 targetPosition = player.position + (displacement * movementMultiplier);

            // Apply smooth movement
            player.position = Vector3.SmoothDamp(player.position, targetPosition, ref velocity, smoothingFactor);
        }
    }

    bool IsGrabbing(XRNode controllerNode)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(controllerNode);

        bool frontTriggerPressed = device.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerValue) && triggerValue;
        bool sideGripPressed = device.TryGetFeatureValue(CommonUsages.gripButton, out bool gripValue) && gripValue;
        bool buttonPressed = (device.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryValue) && primaryValue) ||
                             (device.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryValue) && secondaryValue);

        return frontTriggerPressed && sideGripPressed && buttonPressed;
    }
}