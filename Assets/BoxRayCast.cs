using UnityEngine;

public class BoxRayCast : MonoBehaviour
{
    [Header("BoxCast Settings")]
    public Vector3 halfExtents = new Vector3(0.5f, 0.5f, 0.5f); //Box size
    public float maxDistance = 10f;
    public float hitdistance;
    public LayerMask hitLayers;     // Layerhit

    public float MATH1 = 90;
    public float MATH2 = -5;

    public Camera Handcam;
    public float Distancebox;
    void Update()
    {
        Vector3 direction = transform.forward;
        Distancebox = (Handcam.fieldOfView - MATH1) / MATH2;
        // BoxCast
        if (Physics.BoxCast(transform.position, halfExtents, direction, out RaycastHit hit, transform.rotation, maxDistance, hitLayers))
        {
            hitdistance = maxDistance;
            Debug.DrawRay(transform.position, direction * hitdistance, Color.red);
            //Debug.Log("Hit: " + hit.collider.name + " at distance " + hitdistance);
        }

        if (Physics.Raycast(transform.position, transform.forward, out hit, 100f))
        {
            if (hit.collider.CompareTag("Target"))
            {
                Debug.Log("NEVERGONNAGIVEYOUUP");
            }
        }

    }

    // Visualize the box
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.forward * Distancebox, halfExtents * 2);
    }
}
