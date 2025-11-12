using UnityEngine;

public class CheckAttachedBattery : MonoBehaviour
{
    public GameObject currentBattery;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Battery"))
        {
            currentBattery = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Battery"))
        {
            if (currentBattery == other.gameObject)
            {
                currentBattery = null;
            }
        }
    }
}
