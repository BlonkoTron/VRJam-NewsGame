using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.XR;

public class RecordingManager : MonoBehaviour
{
    public GameObject recordIndicator;

    public BatteryState batteryState;
    public LensChecker lensChecker;
    public Haptics haptics;

    private InputDevice rightController;
    public bool rightTriggerPressed;
    public bool canRecord;

    public Slider batterySlider;
    public Image batteryFillImage;
    public CheckAttachedBattery checkAttachedBattery;
    
    public GameObject lensWarning;
    public GameObject batteryWarning;

    void Awake()
    {
        batteryState = GameObject.Find("Right-Hand").GetComponent<BatteryState>();
        lensChecker = GameObject.Find("SocketCam").GetComponent<LensChecker>();
    }

    void Start()
    {
        // Find the right-hand XR controller
        InitializeRightController();
        ActivateGameObject(lensWarning);
        ActivateGameObject(batteryWarning);

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

        UpdateBatteryLevel();



        // Read trigger value (float 0.0–1.0)
        if (rightController.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            bool isPressed = triggerValue > 0.1f; // Adjust threshold if needed

            // When pressed: activate object
            if (isPressed && !rightTriggerPressed)
            {
                CheckBatteryState();
                rightTriggerPressed = true;
                if (recordIndicator != null && canRecord && lensChecker.lensAttached)
                { 
                    recordIndicator.SetActive(true);
                }
            }

            // When released: deactivate object
            else if (!isPressed && rightTriggerPressed || !lensChecker.lensAttached || !canRecord)
            {
                rightTriggerPressed = false;
                if (recordIndicator != null)
                    recordIndicator.SetActive(false);
            }
        }
    }

    //HERE MR BENJAMIN AAAAAAAAAAAAAAAAAAAAAAAAAAAAAHHHHH
    public void CheckBatteryState()
    {
        if (checkAttachedBattery.currentBattery != null)
        {
            Debug.Log("Checking Battery State");
            if (batteryState.battery_1 && batteryState.battery_2 && checkAttachedBattery.currentBattery.GetComponent<Batterydrain>().BatteryLife > 0)
            {
                canRecord = true;
                Debug.Log("Can Record: " + canRecord);
                if (batteryWarning.activeSelf)
                {
                    DeactivateGameObject(batteryWarning);
                    Debug.Log("Battery Warning Deactivated");
                }
            }
            else
            {
                canRecord = false;
                if (!batteryWarning.activeSelf)
                {
                    ActivateGameObject(batteryWarning);
                }
            }
        }
        else
        {
            canRecord = false;
           if (!batteryWarning.activeSelf)
            {
                ActivateGameObject(batteryWarning);
            }
        }
    }

    public void UpdateBatteryLevel()
    {
        if (batteryState.battery_1 && batteryState.battery_2 && checkAttachedBattery.currentBattery != null)
        {
            batterySlider.value = checkAttachedBattery.currentBattery.GetComponent<Batterydrain>().BatteryLife;

            switch (batterySlider.value)
            {
                case >= 600 and <= 1000:
                    batteryFillImage.color = Color.green;
                    break;

                case >= 300 and <= 599:
                    batteryFillImage.color = Color.yellow;
                    break;

                case >= 1 and <= 299:
                    batteryFillImage.color = Color.red;
                    break;

                default:
                    batteryFillImage.color = Color.green;
                    break;
            }

        }
        else
        {
            batterySlider.value = 0;
        }
    }

   public void ActivateGameObject(GameObject obj)
    {
        obj.SetActive(true);
        haptics.SendHaptic();

    }

    public void DeactivateGameObject(GameObject obj)
    {
        obj.SetActive(false);
        
    }

}
