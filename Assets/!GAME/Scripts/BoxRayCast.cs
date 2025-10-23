using UnityEngine;

public class BoxRayCast : MonoBehaviour
{
    [Header("BoxCast Settings")]
    public Vector3 halfExtents = new Vector3(0.5f, 0.5f, 0.5f); // Box size
    public float maxDistance = 10f; //Maxdistance for boxcast
    public float hitdistance; //Hitdistance for boxcast
    public LayerMask hitLayers;

    [Header("Camera Distance Math")]
    public float MATH1 = 90; //Math for distance calculations
    public float MATH2 = -5; //Math for distance calculations
    public Camera Handcam; //Handcam
    public float Distancebox; //Camdistance float calculation

    private float currentDistance; // dynamically used cast distance

    void Update()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        // FOV-based movement with joystick
        Distancebox = (Handcam.fieldOfView - MATH1) / MATH2;

        // Start each frame with maxDistance as default
        currentDistance = maxDistance;

        // Detect target
        if (Physics.Raycast(origin, direction, out RaycastHit rayHit, maxDistance))
        {
            if (rayHit.collider.CompareTag("Target"))
            {
                // Hit the Target and shortens the cast distance
                hitdistance = rayHit.distance;
                currentDistance = hitdistance;
            }
        }

        // Perform BoxCast
        if (Physics.BoxCast(origin, halfExtents, direction, out RaycastHit boxHit, transform.rotation, currentDistance, hitLayers))
        {
            Debug.DrawRay(origin, direction * boxHit.distance, Color.red);
        }
        else
        {
            Debug.DrawRay(origin, direction * currentDistance, Color.green);
        }
    }

    // Visualize the box in Scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

        // Use Distancebox and gizmo to prevent visual offset
        // So the boxcast does not go over the maxdistance when held back by "target"
        float gizmoDist = Application.isPlaying ? Mathf.Min(Distancebox, currentDistance) : Distancebox;

        Gizmos.DrawWireCube(Vector3.forward * gizmoDist, halfExtents * 2);
    }
}
