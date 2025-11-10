using UnityEngine;

public class LensInteraction : MonoBehaviour
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
    
    [Header("Auto-Attach Settings")]
    [Tooltip("Automatically attach when entering trigger collider")]
    public bool autoAttachOnTrigger = true;
    
    [Tooltip("Only attach to objects on this layer")]
    public LayerMask attachLayer;
    
    [Header("State")]
    public bool isAttached = false;
    
    [Header("Debug")]
    [Tooltip("Show debug logs")]
    public bool debugMode = true;
    
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
            
            if (debugMode)
            {
                Debug.Log($"Detach timer: {detachTimer:F2}/{currentDetachTime:F2}");
            }
            
            if (detachTimer >= currentDetachTime)
            {
                if (debugMode)
                {
                    Debug.Log("Auto-detaching now!");
                }
                Detach();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Auto-attach when entering a trigger collider
        if (autoAttachOnTrigger && !isAttached)
        {
            // Check if the object is on the correct layer
            if (((1 << other.gameObject.layer) & attachLayer) != 0)
            {
                Attach(other.transform);
            }
        }
    }

    /// <summary>
    /// Attach this object to the specified attach point
    /// </summary>
    /// <param name="targetAttachPoint">The transform to attach to</param>
    public void Attach(Transform targetAttachPoint)
    {
        if (isAttached)
        {
            if (debugMode) Debug.Log("Already attached, ignoring attach request");
            return;
        }
        
        attachPoint = targetAttachPoint;
        
        // Store current world position and rotation before parenting
        Vector3 worldPosition = transform.position;
        Quaternion worldRotation = transform.rotation;
        
        // Store current parent before changing it
        originalParent = transform.parent;
        
        // Disable physics while attached
        if (rb != null)
        {
            rb.isKinematic = true;
        }
        
        // Parent to attach point
        transform.SetParent(attachPoint);
        
        // Restore world position and rotation (keeps transform in place)
        transform.position = worldPosition;
        transform.rotation = worldRotation;
        
        isAttached = true;
        
        // Initialize random detach timer
        if (autoDetach)
        {
            detachTimer = 0f;
            currentDetachTime = Random.Range(minDetachTime, maxDetachTime);
            if (debugMode)
            {
                Debug.Log($"Will detach in {currentDetachTime:F2} seconds");
            }
        }
        
        OnAttached();
    }
    
    /// <summary>
    /// Detach this object from its current attach point
    /// </summary>
    public void Detach()
    {
        if (!isAttached)
        {
            if (debugMode) Debug.Log("Not attached, ignoring detach request");
            return;
        }
        
        if (debugMode)
        {
            Debug.Log($"Detaching from {attachPoint?.name ?? "null"}");
        }
        
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
        if (debugMode)
        {
            Debug.Log($"{gameObject.name} attached to {attachPoint.name} | AutoDetach: {autoDetach}");
        }
    }
    
    /// <summary>
    /// Called when object is detached. Override for custom behavior.
    /// </summary>
    protected virtual void OnDetached()
    {
        if (debugMode)
        {
            Debug.Log($"{gameObject.name} detached");
        }
    }
}
