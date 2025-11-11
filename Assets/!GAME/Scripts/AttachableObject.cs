using UnityEngine;

public class AttachableObject : MonoBehaviour
{
    [Header("Attach Settings")]
    [Tooltip("The object this will attach to")]
    public Transform attachPoint;
    
    [Tooltip("Should maintain current rotation when attaching?")]
    public bool maintainRotation = false;
    
    [Tooltip("Position offset from attach point")]
    public Vector3 positionOffset = Vector3.zero;
    
    [Tooltip("Rotation offset from attach point")]
    public Vector3 rotationOffset = Vector3.zero;
    
    [Header("Auto-Detach Settings")]
    [Tooltip("Enable automatic detachment after a random interval")]
    public bool autoDetach = false;
    
    [Tooltip("Minimum time (in seconds) before auto-detach")]
    public float minDetachTime = 2f;
    
    [Tooltip("Maximum time (in seconds) before auto-detach")]
    public float maxDetachTime = 5f;
    
    [Header("State")]
    public bool isAttached = false;
    
    private Transform originalParent;
    private Vector3 originalLocalPosition;
    private Quaternion originalLocalRotation;
    private Rigidbody rb;
    private float detachTimer;
    private float currentDetachTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Store original transform data
        originalParent = transform.parent;
        originalLocalPosition = transform.localPosition;
        originalLocalRotation = transform.localRotation;
    }

    void Update()
    {
        // Handle auto-detach timer
        if (autoDetach && isAttached)
        {
            detachTimer += Time.deltaTime;
            
            if (detachTimer >= currentDetachTime)
            {
                Detach();
            }
        }
    }

    /// <summary>
    /// Attach this object to the specified attach point
    /// </summary>
    /// <param name="targetAttachPoint">The transform to attach to</param>
    public void Attach(Transform targetAttachPoint)
    {
        if (isAttached) return;
        
        attachPoint = targetAttachPoint;
        
        // Disable physics while attached
        if (rb != null)
        {
            rb.isKinematic = true;
        }
        
        // Parent to attach point
        transform.SetParent(attachPoint);
        
        // Set position and rotation
        transform.localPosition = positionOffset;
        
        if (maintainRotation)
        {
            // Keep current world rotation
            transform.rotation = transform.rotation;
        }
        else
        {
            transform.localRotation = Quaternion.Euler(rotationOffset);
        }
        
        isAttached = true;
        
        // Initialize random detach timer
        if (autoDetach)
        {
            detachTimer = 0f;
            currentDetachTime = Random.Range(minDetachTime, maxDetachTime);
        }
        
        OnAttached();
    }
    
    /// <summary>
    /// Detach this object from its current attach point
    /// </summary>
    public void Detach()
    {
        if (!isAttached) return;
        
        // Unparent
        transform.SetParent(originalParent);
        
        // Re-enable physics
        if (rb != null)
        {
            rb.isKinematic = false;
        }
        
        isAttached = false;
        attachPoint = null;
        
        // Reset timer
        detachTimer = 0f;
        
        OnDetached();
    }
    
    /// <summary>
    /// Toggle attach/detach state
    /// </summary>
    /// <param name="targetAttachPoint">The transform to attach to (only used when attaching)</param>
    public void Toggle(Transform targetAttachPoint = null)
    {
        if (isAttached)
        {
            Detach();
        }
        else
        {
            if (targetAttachPoint != null)
            {
                Attach(targetAttachPoint);
            }
            else if (attachPoint != null)
            {
                Attach(attachPoint);
            }
        }
    }
    
    /// <summary>
    /// Called when object is attached. Override for custom behavior.
    /// </summary>
    protected virtual void OnAttached()
    {
        Debug.Log($"{gameObject.name} attached to {attachPoint.name}");
    }
    
    /// <summary>
    /// Called when object is detached. Override for custom behavior.
    /// </summary>
    protected virtual void OnDetached()
    {
        Debug.Log($"{gameObject.name} detached");
    }
}
