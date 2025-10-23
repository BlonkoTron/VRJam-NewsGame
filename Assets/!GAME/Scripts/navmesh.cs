using UnityEngine;
using UnityEngine.AI;

public class NavMesh : MonoBehaviour
{
    public Transform target; // Target
    private NavMeshAgent agent;

    void Start()
    {
        // Get NavMeshAgent component
        agent = GetComponent<NavMeshAgent>();

        if (target != null)
        {
            // Set destination to target
            agent.SetDestination(target.position);
        }
    }

    void Update()
    {
        // Cntinuously update 
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }
}
