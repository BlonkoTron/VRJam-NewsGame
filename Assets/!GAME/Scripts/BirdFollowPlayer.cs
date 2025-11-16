using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class BirdFollowPlayer : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    [SerializeField] private float minDistanceToStopMoving=0.1f;
    [SerializeField] private GameObject explodeParticlePrefab;
    [SerializeField] private Transform target;

    private EventInstance Birdespawn;
    private EventInstance Birddead;

    [SerializeField] private EventReference Birderspawn_1;
    [SerializeField] private EventReference Birderdead_2;

    private bool hasBeenGrabbed=false;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (target==null)
        {
            target = FindAnyObjectByType<PointsRayCast>().gameObject.transform;
        }
        Birdespawn = Audiomanager.instance.PlaySound(Birderspawn_1, transform.position);
    }
    void Update()
    {
        // Check if the positions are approximately equal.
        if (Vector3.Distance(transform.position, target.position) > minDistanceToStopMoving && hasBeenGrabbed==false)
        {
            // Move our position a step closer to the target.
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            transform.LookAt(target.position);
            transform.Rotate(new Vector3(0, 90, 0));
        }
    }
    public void SetHasBeenGrabbed(bool grab)
    {
        hasBeenGrabbed = grab;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (hasBeenGrabbed)
        {
            Instantiate(explodeParticlePrefab,transform.position,Quaternion.identity);
            Destroy(this.gameObject);
            Birddead = Audiomanager.instance.PlaySound(Birderdead_2, transform.position);

        }
        if (collision.gameObject.CompareTag("Grabbable"))
        {
            hasBeenGrabbed = true;
            rb.useGravity = true;
        }
    }

}
