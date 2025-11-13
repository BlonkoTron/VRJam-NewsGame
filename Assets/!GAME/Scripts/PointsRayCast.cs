using UnityEngine;
using UnityEngine.UI;

public class PointsRayCast : MonoBehaviour
{
    [Header("BoxCast Settings")]
    public Vector3 boxHalfExtents = new Vector3(0.5f, 0.5f, 0.5f); // Size of the box
    public float maxDistance = 10f; // Max distance for the boxcast

    [Header("Points System")]
    public float hitDistance;
    public float currentPoints;
    public RecordingManager recordingManager;
    public GameObject recordIndicator;

    public Image camLinesImage;
   

    private void Awake()
    {
        // Fix: assign to the field, not a new local variable
        recordingManager = GetComponent<RecordingManager>();
    }

    private void Update()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        // Perform the boxcast
        if (Physics.BoxCast(origin, boxHalfExtents, direction, out RaycastHit hitInfo, transform.rotation, maxDistance))
        {
            hitDistance = hitInfo.distance;
            

            if (hitInfo.collider.CompareTag("Points"))
            {
                camLinesImage.color= Color.green;

                if (recordIndicator.activeSelf && PointManager.Instance != null)
                {
                    PointManager.Instance.totalPoints += 1;
                }
            }
            else
            {
                camLinesImage.color = Color.white;
            }
        }
    }

    // Optional: visualize the boxcast in Scene view
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.green;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.forward * hitDistance, boxHalfExtents * 2);
    }
}


