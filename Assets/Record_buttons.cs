using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.XR;

public class Quest2RightControllerInput : MonoBehaviour
{
    private bool triggerPressed = false; // track ON/OFF state
    public GameObject recordindicator;

    void Awake()
    {
        recordindicator.SetActive(false);
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

        // === Trigger as ON/OFF ===
        var trigger = rightHand.TryGetChildControl<AxisControl>("trigger");
        if (trigger != null)
        {
            float value = trigger.ReadValue();
            bool isPressed = value > 0.1f;

            // Detect ON
            if (isPressed && !triggerPressed)
            {
                triggerPressed = true;
                Debug.Log("Trigger ON");
                recordindicator.SetActive(true);
            }

            // Detect OFF
            if (!isPressed && triggerPressed)
            {
                triggerPressed = false;
                Debug.Log("Trigger OFF");
                recordindicator.SetActive(false);
            }
        }

        // === Grip ===
        var grip = rightHand.TryGetChildControl<AxisControl>("grip");
        if (grip != null && grip.ReadValue() > 0.1f)
            Debug.Log($"Grip value: {grip.ReadValue()}");

        // === Thumbstick movement ===
        var stick = rightHand.TryGetChildControl<StickControl>("thumbstick");
        if (stick != null && stick.ReadValue() != Vector2.zero)
            Debug.Log($"Thumbstick: {stick.ReadValue()}");

        // === Thumbstick click ===
        if (rightHand.TryGetChildControl<ButtonControl>("thumbstickClicked")?.wasPressedThisFrame == true)
            Debug.Log("Thumbstick clicked");
    }
}
