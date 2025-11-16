using UnityEngine;

public class CheckAttachedBattery : MonoBehaviour
{
    public GameObject currentBattery;
    public RecordingManager recordingManager;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Battery"))
        {
            currentBattery = other.gameObject;
            recordingManager.CheckBatteryState();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Battery"))
        {
            if (currentBattery == other.gameObject)
            {
                currentBattery = null;
                recordingManager.CheckBatteryState();
                
            }
        }
    }
}
