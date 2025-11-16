using UnityEngine;

public class CheckBatteryTwo : MonoBehaviour
{
    public GameObject currentBatteryTwo;
    public RecordingManager recordingManager;

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Battery"))
        {
            Debug.Log("Battery Two Attached");
            currentBatteryTwo = other.gameObject;
            recordingManager.CheckBatteryState();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Battery"))
        {
            if (other.gameObject == currentBatteryTwo)
            {
                Debug.Log("Battery Two Detached");
                currentBatteryTwo = null;
                recordingManager.CheckBatteryState();

            }
        }

    }
}
