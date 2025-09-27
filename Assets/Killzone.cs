using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        BoxRespawn box = other.GetComponent<BoxRespawn>();
        if (box != null)
        {
            box.Respawn();
        }
    }
}
