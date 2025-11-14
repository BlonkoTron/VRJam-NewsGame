using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationToRagdoll : MonoBehaviour
{
    [SerializeField] Collider myCollider;
    [SerializeField] float respawnTime = 30f;
    Rigidbody[] rigidbodies;

    [SerializeField] GameObject DespawnPoof;

    private NavMeshAgent agent;

    bool bIsRagdoll = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        ToggleRagdoll(true);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (!bIsRagdoll && collision.gameObject.tag == "Grabbable")
        {
            ToggleRagdoll(false);
            StartCoroutine(Despawn());

            agent.enabled = false;
        }
    }
    private void ToggleRagdoll(bool bisAnimating)
    {
        bIsRagdoll = !bisAnimating;

        myCollider.enabled = bisAnimating;
        foreach (Rigidbody ragdollBone in rigidbodies)
        {
            ragdollBone.isKinematic = bisAnimating;
        }

        GetComponent<Animator>().enabled = bisAnimating;
        if (bisAnimating)
        {
            RandomAnimation();
        }
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(respawnTime);

        Instantiate(DespawnPoof, transform.position, Quaternion.identity);

        Destroy(gameObject);

    }

    void RandomAnimation()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Walking");

    }
}