using UnityEngine;

public class AttachPoint : MonoBehaviour
{
    [Header("Attach Point Settings")]
    [Tooltip("Currently attached object")]
    public AttachableObject attachedObject;
    
    [Tooltip("Can only attach objects with this tag (leave empty for any)")]
    public string allowedTag = "";
    
    [Tooltip("Maximum distance to auto-attach objects")]
    public float attachDistance = 0.5f;
    
    [Tooltip("Show attach point gizmo in scene")]
    public bool showGizmo = true;
    
    [Tooltip("Gizmo color")]
    public Color gizmoColor = Color.green;

    /// <summary>
    /// Attach an object to this attach point
    /// </summary>
    /// <param name="obj">The attachable object to attach</param>
    /// <returns>True if successfully attached</returns>
    public bool AttachObject(AttachableObject obj)
    {
        if (obj == null) return false;
        
        // Check if tag is allowed
        if (!string.IsNullOrEmpty(allowedTag) && !obj.CompareTag(allowedTag))
        {
            Debug.LogWarning($"Cannot attach {obj.name} - incorrect tag. Expected: {allowedTag}");
            return false;
        }
        
        // Detach current object if one exists
        if (attachedObject != null)
        {
            DetachObject();
        }
        
        // Attach the new object
        obj.Attach(transform);
        attachedObject = obj;
        
        return true;
    }
    
    /// <summary>
    /// Detach the currently attached object
    /// </summary>
    public void DetachObject()
    {
        if (attachedObject != null)
        {
            attachedObject.Detach();
            attachedObject = null;
        }
    }
    
    /// <summary>
    /// Check if an object is within attach distance
    /// </summary>
    /// <param name="obj">The object to check</param>
    /// <returns>True if within range</returns>
    public bool IsInRange(Transform obj)
    {
        return Vector3.Distance(transform.position, obj.position) <= attachDistance;
    }
    
    /// <summary>
    /// Try to find and attach nearby objects
    /// </summary>
    public void TryAttachNearby()
    {
        if (attachedObject != null) return;
        
        AttachableObject[] nearbyObjects = FindObjectsByType<AttachableObject>(FindObjectsSortMode.None);
        
        foreach (var obj in nearbyObjects)
        {
            if (!obj.isAttached && IsInRange(obj.transform))
            {
                if (string.IsNullOrEmpty(allowedTag) || obj.CompareTag(allowedTag))
                {
                    AttachObject(obj);
                    break;
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if (!showGizmo) return;
        
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, 0.05f);
        Gizmos.DrawWireSphere(transform.position, attachDistance);
    }
}
