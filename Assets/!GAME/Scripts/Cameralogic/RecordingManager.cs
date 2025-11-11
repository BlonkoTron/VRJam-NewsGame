using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;

public class RecordingManager : MonoBehaviour
{
    public GameObject recordIndicator;

    public BatteryState batteryState;

    private InputDevice rightController;
    public bool rightTriggerPressed;
    public bool canRecord;

    void Awake()
    {
        batteryState = GameObject.Find("Right-Hand").GetComponent<BatteryState>();
    }

    void Start()
    {
        // Find the right-hand XR controller
        InitializeRightController();

    }

    void InitializeRightController()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            rightController = devices[0];
        }
    }

    void Update()
    {
        // If controller not found (e.g., reconnected), try again
        if (!rightController.isValid)
        {
            InitializeRightController();
        }

        // Read trigger value (float 0.0–1.0)
        if (rightController.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            bool isPressed = triggerValue > 0.1f; // Adjust threshold if needed

            // When pressed: activate object
            if (isPressed && !rightTriggerPressed)
            {
                CheckBatteryState();
                rightTriggerPressed = true;
                if (recordIndicator != null && canRecord)
                    recordIndicator.SetActive(true);

            }

            // When released: deactivate object
            else if (!isPressed && rightTriggerPressed)
            {
                rightTriggerPressed = false;
                if (recordIndicator != null)
                    recordIndicator.SetActive(false);
            }
        }
    }

    public void CheckBatteryState()
    {
        if (batteryState.battery_1 && batteryState.battery_2)
        {
            canRecord = true;
        }
        else
        {
            canRecord = false;
           /* if (recordIndicator != null)
                recordIndicator.SetActive(false);*/
        }
    }

}
