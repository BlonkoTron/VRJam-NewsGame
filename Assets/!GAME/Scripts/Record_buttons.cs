using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;

public class ControllerButtons : MonoBehaviour
{
    public bool triggerToggled = false; // track toggle state
    public bool newState;
    public GameObject recordIndicator;
    public float TargetFOV;

    public Camera RecordCam;

    [SerializeField]
    private float Zoomspeed;

    public static InputFeatureUsage<bool> triggerButton;


    void Awake()
    {
        if (recordIndicator != null)
        {
            recordIndicator.SetActive(false);
        }
    }

    void Update()
    {
        
        // Get the right-hand XR controller
        var rightHand = InputSystem.GetDevice<UnityEngine.InputSystem.XR.XRController>();
        if (rightHand == null) return;

        // === A button ===
        if (rightHand.TryGetChildControl<ButtonControl>("primaryButton")?.wasPressedThisFrame == true)
            Debug.Log("A button pressed");
        
        // === B button ===
        if (rightHand.TryGetChildControl<ButtonControl>("secondaryButton")?.wasPressedThisFrame == true)
            Debug.Log("B button pressed");

        // === Trigger as TOGGLE ===
        var trigger = rightHand.TryGetChildControl<AxisControl>("trigger");
        if (trigger != null)
        {
            if (trigger.ReadValue() > 0.5 && recordIndicator.activeSelf == false)
            {
                recordIndicator.SetActive(true);
            }
            if (trigger.ReadValue() < 0.5 && recordIndicator == true)
            {
                recordIndicator.SetActive(false);
            }



            /*
            // Toggle only when the trigger is pressed this frame (not held down)
            if (trigger.ReadValue() > 0.8f && !triggerToggled) // press threshold
            {
                triggerToggled = true; // prevent multiple toggles on hold
                newState = !recordIndicator.activeSelf;
                recordIndicator.SetActive(newState);
            }

            // Reset guard once trigger is released
            if (trigger.ReadValue() < 0.1f && triggerToggled)
            {
                triggerToggled = false;
            } */
        }

        // === Grip ===
        var grip = rightHand.TryGetChildControl<AxisControl>("grip");
        if (grip != null && grip.ReadValue() > 0.1f)
            Debug.Log($"Grip value: {grip.ReadValue()}");


        
        // === Thumbstick movement ===
        var stick = rightHand.TryGetChildControl<StickControl>("thumbstick");
        if (stick.value.y >= 0.90f)
        {
            TargetFOV = TargetFOV - 1;
            if (TargetFOV <= 40)
            {
                TargetFOV = 40;
            }
            Debug.Log("Down_joystick");
        }
        if (stick.value.y <= -0.90f)
        {
            TargetFOV = TargetFOV + 1;
            if (TargetFOV >= 90)
            {
                TargetFOV = 90;
            }
            Debug.Log("Up_joystick");
            
        }
        RecordCam.fieldOfView = Mathf.Lerp(RecordCam.fieldOfView, TargetFOV, Time.deltaTime * Zoomspeed);

        // === Thumbstick click ===
        if (rightHand.TryGetChildControl<ButtonControl>("thumbstickClicked")?.wasPressedThisFrame == true)
            Debug.Log("Thumbstick clicked");
    }
}
