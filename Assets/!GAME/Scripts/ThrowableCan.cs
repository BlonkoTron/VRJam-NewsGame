using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ThrowableCan : MonoBehaviour
{
    private Vector3 startPosition;
    private bool hasBeenGrabbed = false;
    [SerializeField] private float waveSpeed = 1;
    [SerializeField] private float waveAmount = 0.1f;
    [SerializeField] private ParticleSystem shineParticle;
    private Rigidbody rb;

    public UnityAction onGrabbed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    private void Update()
    {
        if (!hasBeenGrabbed)
        {
            transform.localPosition = startPosition + new Vector3(0, Mathf.Sin(Time.time * waveSpeed) * waveAmount, 0);
        }
    }
    public void OnGrab()
    {
        hasBeenGrabbed = true;
        rb.useGravity = true;
        shineParticle.Stop();
        onGrabbed.Invoke();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Grabbable")) {
            OnGrab();
        }
    }

}
