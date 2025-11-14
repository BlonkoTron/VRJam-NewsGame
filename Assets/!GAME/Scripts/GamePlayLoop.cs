using UnityEngine;

public class GamePlayLoop : MonoBehaviour
{
    [SerializeField] public GameObject[] objectsToMonitor;
    [SerializeField] public Animator animator;
    [SerializeField] public string allObjectsDisabledParameterName = "AllDisabled"; // Parameter to set when all objects are disabled
    [SerializeField] public string animationStateName = "NextAnimation";
    [SerializeField] public string animationParameterName = "AnimationComplete"; // Bool parameter to check
    [SerializeField] public float animationDuration = 5f; // Duration in seconds before setting AnimationComplete
    [SerializeField] public float secondTimerDuration = 17.5f; // Duration for second timer
    [SerializeField] public float thirdTimerDuration = 34.5f;
    [SerializeField] public GameObject objectToDisable;
    [SerializeField] public GameObject objectToEnable;
    [SerializeField] public GameObject BirdAnimationOff;
    [SerializeField] public GameObject HouseAnimationON;
    
    [SerializeField] public GameObject Door;
    [SerializeField] public GameObject KajiuAnimation;
    
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
            Debug.Log($"<color=cyan>All objects disabled! Starting {animationDuration} second timer</color>");
            StartCoroutine(AnimationTimerCoroutine());
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

    private System.Collections.IEnumerator AnimationTimerCoroutine()
    {
        Debug.Log($"Timer started for {animationDuration} seconds");
        yield return new WaitForSeconds(animationDuration);
        
        Debug.Log("<color=yellow>Timer finished, setting AnimationComplete to true</color>");
        animator.SetBool(animationParameterName, true);
        ToggleObjectsDelayed();
        // Start second timer
        
        Debug.Log($"<color=cyan>Starting second timer for {secondTimerDuration} seconds</color>");
        yield return new WaitForSeconds(secondTimerDuration);
        
        BulidingAnimationOn();
        Debug.Log("<color=yellow>Second timer finished, toggling objects</color>");

        yield return new WaitForSeconds(thirdTimerDuration);
        Kajiu();
        
    }

    private void Kajiu()
    {
        HouseAnimationON.SetActive(false);
        Door.SetActive(false);
        KajiuAnimation.SetActive(true);
    }

    private void BulidingAnimationOn()
    {
        if (objectToDisable != null)
        {
            HouseAnimationON.SetActive(true);
            objectToEnable.SetActive(false);
        }
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
