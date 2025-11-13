using UnityEngine;
using UnityEngine.UI;

public class AttackingProtester : ProtesterBehaviour
{

    [Header("Insert the Handheld Camera GameObject From the Scene HERE")]
    private Transform camTransform;
    [SerializeField] private float distanceToCam;
    [SerializeField] private float attackingSpd;

    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        camTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;

        waypoints = ProtesterHivemind.Instance.attackWaypoints;

        agent.speed = speed;

        RandomizeApperance();

        if (waypoints.Length != 0)
        {
            currentWaypointIndex = GetClosestWaypointIndex();

            // Set the first destination
            if (waypoints.Length > 0)
            {
                agent.SetDestination(waypoints[currentWaypointIndex].position);
            }
        }

    }

    private void Update()
    {
        if(currentWaypointIndex != waypoints.Length - 1) 
        { 
            WaypointCheck();
        }
        else
        {
            AttackCam();
        }



    }

    private void AttackCam()
    {

        agent.speed = attackingSpd;
        if (!agent.pathPending && Vector3.Distance(transform.position, camTransform.position) <= distanceToCam)
        {
            agent.SetDestination(new Vector3(camTransform.position.x, transform.position.y, camTransform.position.z));
        }
            
        

    }
}
