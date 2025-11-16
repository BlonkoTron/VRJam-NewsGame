using UnityEngine;

public class CheckBatteryOne : MonoBehaviour
{
    public GameObject currentBatteryOne;
    public RecordingManager recordingManager;

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Battery"))
        {
            Debug.Log("Battery One Attached");
            currentBatteryOne = other.gameObject;
            recordingManager.CheckBatteryState();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Battery"))
        {
            if (other.gameObject == currentBatteryOne)
            {
                Debug.Log("Battery One Detached");
                currentBatteryOne = null;
                recordingManager.CheckBatteryState();

            }
        }
        
    }
}
