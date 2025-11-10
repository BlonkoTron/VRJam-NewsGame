using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.XR;

public class ControllerButtons : MonoBehaviour
{
    [Header("Recording Settings")]
    public GameObject recordIndicator;
    public Camera recordCam;
    public float zoomSpeed = 5f;
    public float targetFOV = 60f;

    [Header("State Tracking")]
    public bool newState = false;
    private bool triggerHeld = false;

    private XRController rightController;

    private const float TriggerPressThreshold = 0.8f;
    private const float TriggerReleaseThreshold = 0.1f;

    private void Awake()
    {
        if (recordIndicator != null)
            recordIndicator.SetActive(false);

        // Get the right-hand XR controller
        rightController = InputSystem.GetDevice<XRController>();
    }

    private void Update()
    {
        if (rightController == null)
        {
            rightController = InputSystem.GetDevice<XRController>();
            if (rightController == null) return;
        }

        HandleButtons();
        HandleTriggerToggle();
        HandleGrip();
        HandleThumbstick();
    }

    private void HandleButtons()
    {
        // A Button
        if (GetButtonPress("primaryButton"))
            Debug.Log("A button pressed");

        // B Button
        if (GetButtonPress("secondaryButton"))
            Debug.Log("B button pressed");

        // Thumbstick Click
        if (GetButtonPress("thumbstickClicked"))
            Debug.Log("Thumbstick clicked");
    }

    private void HandleTriggerToggle()
    {
        var trigger = rightController.TryGetChildControl<AxisControl>("trigger");
        if (trigger == null) return;

        float triggerValue = trigger.ReadValue();

        // When trigger is pressed beyond threshold and not already held → toggle state
        if (triggerValue > TriggerPressThreshold && !triggerHeld)
        {
            triggerHeld = true;
            newState = !newState;

            if (recordIndicator != null)
                recordIndicator.SetActive(newState);

            Debug.Log($"Trigger toggled → newState = {newState}");
        }

        // Reset once trigger is released
        if (triggerValue < TriggerReleaseThreshold && triggerHeld)
        {
            triggerHeld = false;
        }
    }

    private void HandleGrip()
    {
        var grip = rightController.TryGetChildControl<AxisControl>("grip");
        if (grip != null && grip.ReadValue() > 0.1f)
            Debug.Log($"Grip value: {grip.ReadValue()}");
    }

    private void HandleThumbstick()
    {
        var stick = rightController.TryGetChildControl<StickControl>("thumbstick");
        if (stick == null) return;

        if (stick.value.y >= 0.9f)
        {
            targetFOV = Mathf.Max(targetFOV - 1f, 40f);
            Debug.Log("Zooming in");
        }
        else if (stick.value.y <= -0.9f)
        {
            targetFOV = Mathf.Min(targetFOV + 1f, 90f);
            Debug.Log("Zooming out");
        }

        if (recordCam != null)
            recordCam.fieldOfView = Mathf.Lerp(recordCam.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    }

    // --- Utility ---
    private bool GetButtonPress(string controlName)
    {
        return rightController?.TryGetChildControl<ButtonControl>(controlName)?.wasPressedThisFrame == true;
    }
}
