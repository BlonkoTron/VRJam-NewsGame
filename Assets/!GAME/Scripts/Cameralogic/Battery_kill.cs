using UnityEngine;

public class Battery_kill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            Destroy(gameObject);
        }
    }
}
