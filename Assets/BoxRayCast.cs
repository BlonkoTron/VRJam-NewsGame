using UnityEngine;

public class BoxRayCast : MonoBehaviour
{
    [Header("BoxCast Settings")]
    public Vector3 halfExtents = new Vector3(0.1f, 0.1f, 0.1f); //Box size
    public float maxDistance = 10f;
    public float hitdistance;
    public LayerMask hitLayers;     // Layerhit

    void Update()
    {
        Vector3 direction = transform.forward;

        // BoxCast
        if (Physics.BoxCast(transform.position, halfExtents, direction, out RaycastHit hit, transform.rotation, maxDistance, hitLayers))
        {
            hitdistance = hit.distance;
            Debug.DrawRay(transform.position, direction * hitdistance, Color.red);
            Debug.Log("Hit: " + hit.collider.name + " at distance " + hitdistance);
        }
        else
        {
            hitdistance = maxDistance;
            Debug.DrawRay(transform.position, direction * maxDistance, Color.red);
        }
    }

    // Visualize the box
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.forward * hitdistance, halfExtents * 2);
    }
}
