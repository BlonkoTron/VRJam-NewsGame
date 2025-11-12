using UnityEngine;

public class LensChecker : MonoBehaviour
{
    public bool lensAttached;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lens"))
        {
            lensAttached = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Lens"))
        {
            lensAttached = false;
        }
    }

}
