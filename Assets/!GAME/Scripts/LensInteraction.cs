using UnityEngine;
using System.Collections;
using FMOD.Studio;
using FMODUnity;

public class LensInteraction : MonoBehaviour
{
    [Header("Script Disable Settings")]
    [Tooltip("The script to temporarily disable")]
    public MonoBehaviour scriptToDisable;
    
    [Tooltip("Minimum time (in seconds) script stays enabled")]
    public float minEnabledTime = 3f;
    
    [Tooltip("Maximum time (in seconds) script stays enabled")]
    public float maxEnabledTime = 5f;
    
    [Tooltip("Minimum time (in seconds) to keep script disabled")]
    public float minDisableTime = 2f;
    
    [Tooltip("Maximum time (in seconds) to keep script disabled")]
    public float maxDisableTime = 4f;
    
    [Tooltip("Should pick a random time within the range?")]
    public bool useRandomTime = true;
    
    [Tooltip("Start the cycle automatically on Start?")]
    public bool autoStart = true;
    
    [Header("Debug")]
    [Tooltip("Show debug logs")]
    public bool debugMode = true;
    
    private bool isRunning = false;
    private Coroutine cycleCoroutine;

    private EventInstance Campop;
    [SerializeField] private EventReference Campopper;

    void Start()
    {
        if (autoStart)
        {
            StartCycle();
        }
    }

    /// <summary>
    /// Start the enable/disable cycle
    /// </summary>
    public void StartCycle()
    {
        if (scriptToDisable == null)
        {
            if (debugMode) Debug.LogWarning("No script assigned to disable!");
            return;
        }
        
        if (isRunning)
        {
            if (debugMode) Debug.Log("Cycle already running");
            return;
        }
        
        cycleCoroutine = StartCoroutine(DisableEnableCycle());
    }
    
    /// <summary>
    /// Stop the enable/disable cycle
    /// </summary>
    public void StopCycle()
    {
        if (cycleCoroutine != null)
        {
            StopCoroutine(cycleCoroutine);
            cycleCoroutine = null;
        }
        
        isRunning = false;
        
        // Re-enable the script when stopping
        if (scriptToDisable != null)
        {
            scriptToDisable.enabled = true;
        }
        
        if (debugMode)
        {
            Debug.Log("Cycle stopped");
        }
    }
    
    /// <summary>
    /// Coroutine that continuously cycles between enabled and disabled states
    /// </summary>
    private IEnumerator DisableEnableCycle()
    {
        if (scriptToDisable == null) yield break;
        
        isRunning = true;
        
        if (debugMode)
        {
            Debug.Log("Starting enable/disable cycle");
        }
        
        while (true)
        {
            // Calculate enabled duration
            float enabledDuration;
            if (useRandomTime)
            {
                enabledDuration = Random.Range(minEnabledTime, maxEnabledTime);
            }
            else
            {
                enabledDuration = minEnabledTime;
            }
            
            // Make sure script is enabled
            scriptToDisable.enabled = true;
            
            if (debugMode)
            {
                Debug.Log($"Script enabled for {enabledDuration:F2} seconds");
            }
            
            // Wait while enabled
            yield return new WaitForSeconds(enabledDuration);
            
            // Calculate disabled duration
            float disabledDuration;
            if (useRandomTime)
            {
                disabledDuration = Random.Range(minDisableTime, maxDisableTime);
            }
            else
            {
                disabledDuration = minDisableTime;
            }
            
            // Disable the script
            scriptToDisable.enabled = false;
            Campop = Audiomanmove.instance.PlaySound(Campopper, transform.position);

            if (debugMode)
            {
                Debug.Log($"Script disabled for {disabledDuration:F2} seconds");
            }
            
            // Wait while disabled
            yield return new WaitForSeconds(disabledDuration);
        }
    }
}
