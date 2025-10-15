using UnityEngine;

public class BoxRayCast : MonoBehaviour
{
    [Header("BoxCast Settings")]
    public Vector3 halfExtents = new Vector3(0.5f, 0.5f, 0.5f); // Box size
    public float maxDistance = 10f;
    public float hitdistance;
    public LayerMask hitLayers;

    [Header("Camera Distance Math")]
    public float MATH1 = 90;
    public float MATH2 = -5;
    public Camera Handcam;
    public float Distancebox;

    private float currentDistance; // dynamically used cast distance

    void Update()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        // Maintain your FOV-based movement
        Distancebox = (Handcam.fieldOfView - MATH1) / MATH2;

        // Start each frame with maxDistance as default
        currentDistance = maxDistance;

        // --- Raycast first to detect target ---
        if (Physics.Raycast(origin, direction, out RaycastHit rayHit, maxDistance))
        {
            if (rayHit.collider.CompareTag("Target"))
            {
                // Hit the Target → shorten cast distance
                hitdistance = rayHit.distance;
                currentDistance = hitdistance;
            }
        }

        // --- Perform BoxCast using the possibly shortened distance ---
        if (Physics.BoxCast(origin, halfExtents, direction, out RaycastHit boxHit, transform.rotation, currentDistance, hitLayers))
        {
            Debug.DrawRay(origin, direction * boxHit.distance, Color.red);
        }
        else
        {
            Debug.DrawRay(origin, direction * currentDistance, Color.green);
        }
    }

    // --- Visualize the box in Scene view ---
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

        // Use Distancebox (camera-based movement) for visual offset
        // But clamp it so it doesn’t go past currentDistance when target is hit
        float gizmoDist = Application.isPlaying ? Mathf.Min(Distancebox, currentDistance) : Distancebox;

        Gizmos.DrawWireCube(Vector3.forward * gizmoDist, halfExtents * 2);
    }
}
