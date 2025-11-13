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
    [SerializeField] private GameObject particles;
    private ParticleSystem particleSystem;

    private Color gold = new Color(255f, 255f, 0f);

    private void Awake()
    {
        // Fix: assign to the field, not a new local variable
        recordingManager = GetComponent<RecordingManager>();
        particleSystem = particles.GetComponent<ParticleSystem>();
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
            else if (hitInfo.collider.CompareTag("OneTimePoints"))
            {
                camLinesImage.color = gold;
               
                if (recordIndicator.activeSelf && PointManager.Instance != null)
                {
                        TargetHit(hitInfo.collider.gameObject);
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

    void TargetHit(GameObject obj)
    {
            obj.tag = "Untagged";
            PointManager.Instance.totalPoints += 2500;
            particleSystem.Play();
    }
}


