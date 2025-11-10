using UnityEngine;

public class PointsRayCast : MonoBehaviour
{
    public float maxDistance = 10f; //Maxdistance for boxcast
    public float hitdistance; //Hitdistance for boxcast
    public float currentPoints;
    public RecordingManager recordingManager;


    private void Awake()
    {
        RecordingManager recordingManager = GetComponent<RecordingManager>();
    }

    private void Update()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        

        if (Physics.Raycast(origin, direction, out RaycastHit rayHit, maxDistance))
        {
            if (rayHit.collider.CompareTag("Points") && recordingManager.rightTriggerPressed)
            {
                currentPoints += 1;
            }
        }
    }
}
    
    
