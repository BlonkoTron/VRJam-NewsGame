using UnityEngine;

public class LensChecker : MonoBehaviour
{
    public bool lensAttached;
    public RecordingManager recordingManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lens"))
        {
            lensAttached = true;
            if (recordingManager.lensWarning.activeSelf)
            {
                recordingManager.DeactivateGameObject(recordingManager.lensWarning);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Lens"))
        {
            lensAttached = false;
            if (!recordingManager.lensWarning.activeSelf)
            {
                recordingManager.ActivateGameObject(recordingManager.lensWarning);
            }
        }
    }

}
