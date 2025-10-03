using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.XR;

public class ControllerButtons : MonoBehaviour
{
    public bool triggerToggled = false; // track toggle state
    public bool newState;
    public GameObject recordIndicator;
    public float TargetFOV;

    public Camera RecordCam;

    [SerializeField]
    private float ZoomFOV;
    [SerializeField]
    private float NormalFOV;
    [SerializeField]
    private float Zoomspeed;

    

    void Awake()
    {
        if (recordIndicator != null)
            recordIndicator.SetActive(false);
    }

    private void Start()
    {
        //RecordCam = GetComponent<Camera>();
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
            // Toggle only when the trigger is pressed this frame (not held down)
            if (trigger.ReadValue() > 0.8f && !triggerToggled) // press threshold
            {
                triggerToggled = true; // prevent multiple toggles on hold
                newState = !recordIndicator.activeSelf;
                recordIndicator.SetActive(newState);
                //Debug.Log("Trigger toggled: " + newState);
            }

            // Reset guard once trigger is released
            if (trigger.ReadValue() < 0.1f && triggerToggled)
            {
                triggerToggled = false;
            }
        }

        // === Grip ===
        var grip = rightHand.TryGetChildControl<AxisControl>("grip");
        if (grip != null && grip.ReadValue() > 0.1f)
            Debug.Log($"Grip value: {grip.ReadValue()}");


        
        // === Thumbstick movement ===
        var stick = rightHand.TryGetChildControl<StickControl>("thumbstick");
        if (stick != null && stick.ReadValue() != Vector2.zero)
        {
            //Debug.Log($"Thumbstick: {stick.ReadValue()}");
        }
        if (stick != null && stick.value.y >= 0.90)
        {
            TargetFOV = ZoomFOV;
            Debug.Log("UPUPUP");
        }
        if (stick != null && stick.value.y <= -0.90)
        {
            TargetFOV = NormalFOV;
            Debug.Log("DOWNDOWNDOWN");
        }

        RecordCam.fieldOfView = Mathf.Lerp(RecordCam.fieldOfView, TargetFOV, Time.deltaTime * Zoomspeed);

        // === Thumbstick click ===
        if (rightHand.TryGetChildControl<ButtonControl>("thumbstickClicked")?.wasPressedThisFrame == true)
            Debug.Log("Thumbstick clicked");
    }
}
