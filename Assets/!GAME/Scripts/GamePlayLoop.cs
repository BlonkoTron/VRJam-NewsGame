using UnityEngine;

public class GamePlayLoop : MonoBehaviour
{
    [SerializeField] public GameObject[] objectsToMonitor;
    [SerializeField] public Animator animator;
    [SerializeField] public string allObjectsDisabledParameterName = "AllDisabled"; // Parameter to set when all objects are disabled
    [SerializeField] public string animationStateName = "NextAnimation";
    [SerializeField] public string animationParameterName = "AnimationComplete"; // Bool parameter to check
    [SerializeField] public GameObject objectToDisable;
    [SerializeField] public GameObject objectToEnable;
    
    private bool animationTriggered = false;
    private bool checkingForCompletion = false;
    private float animationStartTime = 0f;
    private bool lastAllDisabledState = false;
    private int lastActiveCount = -1;

    void Update()
    {
        // Exit early if no animator assigned
        if (!animator) return;
        // Update the animator parameter based on object states
        bool allDisabled = AreAllObjectsDisabled();
        if (allDisabled != lastAllDisabledState)
        {
            animator.SetBool(allObjectsDisabledParameterName, allDisabled);
            
            // Verify the parameter was set
            bool verifyValue = animator.GetBool(allObjectsDisabledParameterName);
            Debug.Log($"Verified parameter value: {verifyValue}");
            
            lastAllDisabledState = allDisabled;
        }

        if (!animationTriggered && AreAllObjectsDisabled())
        {
            animationTriggered = true;
            checkingForCompletion = true;
        }

        // Check if animation has finished by checking the normalized time
        if (checkingForCompletion)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            Debug.Log($"Checking animation: IsName={stateInfo.IsName(animationStateName)}, NormalizedTime={stateInfo.normalizedTime}");
            if (stateInfo.IsName(animationStateName) && stateInfo.normalizedTime >= 1.0f)
            {
                checkingForCompletion = false;
                Debug.Log("Animation finished, toggling objects");
                Invoke(nameof(ToggleObjectsDelayed), 0.1f);
            }
        }
    }

    private bool AreAllObjectsDisabled()
    {
        if (objectsToMonitor == null || objectsToMonitor.Length == 0)
        {
            Debug.LogWarning("objectsToMonitor is null or empty!");
            return false;
        }

        int activeCount = 0;
        foreach (GameObject obj in objectsToMonitor)
        {
            if (obj != null && obj.activeInHierarchy)
            {
                activeCount++;
            }
        }
        
        // Only log when count changes
        if (activeCount != lastActiveCount)
        {
            Debug.Log($"Objects monitored: {objectsToMonitor.Length}, Active: {activeCount}, All disabled: {activeCount == 0}");
            lastActiveCount = activeCount;
        }
        
        return activeCount == 0;
    }

    private void ToggleObjectsDelayed()
    {
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false);
        }
        
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);
        }
    }

    // Optional: Reset the trigger if you need to check again
    public void ResetTrigger()
    {
        animationTriggered = false;
    }
}
