using UnityEngine;

public class BirdFollowPlayer : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    [SerializeField] private float minDistanceToStopMoving=0.1f;
    [SerializeField] private GameObject explodeParticlePrefab;
    private Transform target;

    private bool hasBeenGrabbed=false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = FindAnyObjectByType<CharacterController>().transform;
    }
    void Update()
    {
        // Check if the positions are approximately equal.
        if (Vector3.Distance(transform.position, target.position) > minDistanceToStopMoving)
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
        }
    }

}
