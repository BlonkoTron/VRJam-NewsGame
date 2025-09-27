using UnityEngine;

public class BoxRespawn : MonoBehaviour
{
    private Vector3 startPosition; 
    private Quaternion startRotation;

    void Start()
    {
        // Save spawn
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    public void Respawn()
    {
        // Reset spawn/pos/rot
        transform.position = startPosition;
        transform.rotation = startRotation;

        // Reset physics
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}


