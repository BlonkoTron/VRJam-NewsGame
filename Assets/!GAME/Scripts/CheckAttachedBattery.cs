using UnityEngine;

public class CheckAttachedBattery : MonoBehaviour
{
    public GameObject currentTopBattery;
    public GameObject currentBottomBattery;
    public bool isTopBattery;
    public bool isBottomBattery;
    public RecordingManager recordingManager;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Battery") && isTopBattery)
        {
            currentTopBattery = other.gameObject;
            recordingManager.CheckBatteryState();
        }
        if (other.gameObject.CompareTag("Battery") && isBottomBattery)
        {
            currentBottomBattery = other.gameObject;
            recordingManager.CheckBatteryState();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Battery") && isTopBattery)
        {
            if (currentTopBattery == other.gameObject)
            {
                currentTopBattery = null;
                recordingManager.CheckBatteryState();
                
            }
        }
        if (other.gameObject.CompareTag("Battery") && isBottomBattery)
        {
            if (currentBottomBattery == other.gameObject)
            {
                currentBottomBattery = null;
                recordingManager.CheckBatteryState();
            }
        }
    }
}
