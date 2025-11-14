using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using FMOD;
using System.ComponentModel;

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

    [SerializeField] public float NewsIntroStart = 10f;
    [SerializeField] public GameObject objectToDisable;
    [SerializeField] public GameObject objectToEnable;
    [SerializeField] public GameObject BirdAnimationOff;
    [SerializeField] public GameObject HouseAnimationON;
    
    [SerializeField] public GameObject Door;
    [SerializeField] public GameObject KajiuAnimation;


    private EventInstance NewsMan_1;

    [SerializeField] private EventReference NewsMan_1Event;

    
    
    
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
            
            lastAllDisabledState = allDisabled;
        }

        if (!animationTriggered && AreAllObjectsDisabled())
        {
            animationTriggered = true;
            StartCoroutine(AnimationTimerCoroutine());
        }
    }

    private bool AreAllObjectsDisabled()
    {
        if (objectsToMonitor == null || objectsToMonitor.Length == 0)
        {
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

            lastActiveCount = activeCount;
        }
        
        return activeCount == 0;
    }

    private System.Collections.IEnumerator AnimationTimerCoroutine()
    {
        
        yield return new WaitForSeconds(NewsIntroStart);
        NewsMan_1 = Audiomanager.instance.PlaySound(NewsMan_1Event, transform.position);

        yield return new WaitForSeconds(animationDuration);

        animator.SetBool(animationParameterName, true);
        ToggleObjectsDelayed();
        // Start second timer

        yield return new WaitForSeconds(secondTimerDuration);
        

        BulidingAnimationOn();

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
