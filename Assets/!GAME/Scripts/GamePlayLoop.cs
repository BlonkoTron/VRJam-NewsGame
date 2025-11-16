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
    [SerializeField] public string pointsParameterName = "Points"; // Float parameter to track points in animator
    [SerializeField] public float TalkBeforeMayor = 22f;
    [SerializeField] public float animationDuration = 57.5f; // Duration in seconds before setting AnimationComplete
    [SerializeField] public float secondTimerDuration = 17.5f; // Duration for second timer
    [SerializeField] public float thirdTimerDuration = 34.5f;

    [SerializeField] public float TakenByBird = 18f;

    [SerializeField] public float NewsIntroStart = 10f;
    [SerializeField] public GameObject objectToDisable;
    [SerializeField] public GameObject objectToEnable;
    [SerializeField] public GameObject BirdAnimationOff;
    [SerializeField] public GameObject HouseAnimationON;
    
    [SerializeField] public GameObject Door;
    [SerializeField] public GameObject KajiuAnimation;

    public GameObject birdSpawner;

    public GameObject mayorObject;

    private EventInstance NewsMan_1;
    private EventInstance NewsMan_2;
    private EventInstance NewsMan_3;
    private EventInstance NewsMan_4;
    private EventInstance NewsMan_5;
    private EventInstance NewsMan_6;
    private EventInstance NewsMan_7;
    private EventInstance KajiuSound;

    [SerializeField] private EventReference NewsMan_1Event;
    [SerializeField] private EventReference NewsMan_2Event;
    [SerializeField] private EventReference NewsMan_3Event;
    [SerializeField] private EventReference PointsSoundEvent;
    [SerializeField] private EventReference MayorTalkEvent;
    [SerializeField] private EventReference KajiuSoundEvent;

    [SerializeField] private EventReference NewsMan_4Event;
    [SerializeField] private EventReference NewsMan_5Event;
    [SerializeField] private EventReference NewsMan_6Event;
    [SerializeField] private EventReference NewsMan_7Event;

    private float lastPointsValue = 0f;
    private bool pointsSoundPlayed = false;

    private bool NextSequenceStarted = false;
    
    
    private bool animationTriggered = false;
    private bool checkingForCompletion = false;
    private float animationStartTime = 0f;
    private bool lastAllDisabledState = false;
    private int lastActiveCount = -1;

    void Start()
    {
        StartCoroutine(startSequenceCoroutine());
    }

    private System.Collections.IEnumerator startSequenceCoroutine()
    {
        yield return new WaitForSeconds(NewsIntroStart);
        OnAnimationStart();
    }
    void Update()
    {
        // Exit early if no animator assigned
        if (!animator) return;
        
        // Check if points from PointManager changed to greater than zero
        if (PointManager.Instance != null)
        {
            float currentPoints = PointManager.Instance.totalPoints;
            
            // Update animator parameter with current points
            animator.SetFloat(pointsParameterName, currentPoints);
            
            if (!pointsSoundPlayed && currentPoints > 0f && lastPointsValue == 0f)
            {
                // Enable all objects to monitor and set NextSequenceStarted once
                if (!NextSequenceStarted && objectsToMonitor != null && objectsToMonitor.Length > 0)
                {
                    foreach (GameObject obj in objectsToMonitor)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(true);
                        }
                    }
                    NextSequenceStarted = true;
                }
                
                Audiomanager.instance.PlaySound(PointsSoundEvent, transform.position);
                pointsSoundPlayed = true;
                UnityEngine.Debug.Log($"<color=green>Points sound played! Points: {currentPoints}</color>");
            }
            lastPointsValue = currentPoints;
        }
        
        // Update the animator parameter based on object states
        bool allDisabled = AreAllObjectsDisabled();
        if (allDisabled != lastAllDisabledState)
        {
            animator.SetBool(allObjectsDisabledParameterName, allDisabled);
            
            // Verify the parameter was set
            bool verifyValue = animator.GetBool(allObjectsDisabledParameterName);
            
            lastAllDisabledState = allDisabled;
        }

        if (!animationTriggered && AreAllObjectsDisabled() && NextSequenceStarted)
        {
            animationTriggered = true;
            StartCoroutine(AnimationTimerCoroutine());
        }
    }

    // Called when the animation sequence starts - add your custom logic here
    private void OnAnimationStart()
    {
        NewsMan_1 = Audiomanager.instance.PlaySound(NewsMan_1Event, transform.position);
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
        // Get points from PointManager and set in animator
        if (PointManager.Instance != null)
        {
            
            float currentPoints = PointManager.Instance.totalPoints;
            animator.SetFloat(pointsParameterName, currentPoints);
            UnityEngine.Debug.Log($"<color=cyan>Set animator points parameter to: {currentPoints}</color>");
        }
        else
        {
            UnityEngine.Debug.LogWarning("PointManager.Instance is null!");
        }

        
        NewsMan_2 = Audiomanager.instance.PlaySound(NewsMan_2Event, transform.position);
        
        yield return new WaitForSeconds(TalkBeforeMayor);
        Audiomanager.instance.PlaySound(MayorTalkEvent, transform.position);
        birdSpawner.SetActive(true);
        mayorObject.SetActive(true);
        

        yield return new WaitForSeconds(animationDuration);

        NewsMan_4 = Audiomanager.instance.PlaySound(NewsMan_4Event, transform.position);
        
        animator.SetBool(animationParameterName, true);
        ToggleObjectsDelayed();
        // Start second timer

        yield return new WaitForSeconds(secondTimerDuration);
        
        NewsMan_5 = Audiomanager.instance.PlaySound(NewsMan_5Event, transform.position);
        BulidingAnimationOn();

        yield return new WaitForSeconds(thirdTimerDuration);
        Kajiu();
        NewsMan_6 = Audiomanager.instance.PlaySound(NewsMan_6Event, transform.position);
        KajiuSound = Audiomanager.instance.PlaySound(KajiuSoundEvent, transform.position);

        yield return new WaitForSeconds(TakenByBird);
        NewsMan_7 = Audiomanager.instance.PlaySound(NewsMan_7Event, transform.position);
        
    }

    private void Kajiu()
    {
        HouseAnimationON.SetActive(false);
        Door.SetActive(false);
        KajiuAnimation.SetActive(true);
        birdSpawner.SetActive(false);
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
