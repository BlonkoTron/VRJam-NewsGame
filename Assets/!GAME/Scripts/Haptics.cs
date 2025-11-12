using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Haptics : MonoBehaviour
{
    [Header("Controller Settings")]
    public XRNode controllerNode = XRNode.RightHand; // Or XRNode.LeftHand

    [Header("Haptic Settings")]
    [Range(0f, 1f)] public float amplitude = 0.8f; // Strength of vibration
    public float duration = 0.2f; // Seconds

    public void SendHaptic()
    {
        // Get the controller device
        InputDevice device = InputDevices.GetDeviceAtXRNode(controllerNode);

        if (device.isValid)
        {
            // Send a simple haptic impulse
            device.SendHapticImpulse(0, amplitude, duration);
        }
        else
        {
            Debug.LogWarning("OpenXRHaptics: Controller not found or not valid.");
        }
    }
}
