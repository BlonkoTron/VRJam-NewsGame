using UnityEngine;

public class PointsRayCast : MonoBehaviour
{
    public float maxDistance = 10f; //Maxdistance for boxcast
    public float hitdistance; //Hitdistance for boxcast
    public float currentPoints;

    private void Update()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        

        if (Physics.Raycast(origin, direction, out RaycastHit rayHit, maxDistance))
        {
            if (rayHit.collider.CompareTag("Points"))
            {
                currentPoints += 1;
            }
        }
    }
}
    
    
