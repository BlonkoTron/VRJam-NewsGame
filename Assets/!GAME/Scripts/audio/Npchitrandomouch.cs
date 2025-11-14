using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Npchitrandomouch : MonoBehaviour
{
    [SerializeField] private List<EventReference> audioLines = new List<EventReference>();
    [SerializeField] private EventReference canhit;

    public bool test;


    void Update()
    {
        if (test)
        {
            PlayRandomOuch();
            test = false;
        }
    }
    
    public void PlayRandomOuch()
    {
        if (audioLines == null || audioLines.Count == 0)
        {
            Debug.LogWarning("No audio lines assigned to Npchitrandomouch!");
            return;
        }

        // pick a random index within range
        int randomIndex = Random.Range(0, audioLines.Count);

        // play the random "ouch" sound
        RuntimeManager.PlayOneShot(audioLines[randomIndex], transform.position);

        // play the "canhit" sound simultaneously
        RuntimeManager.PlayOneShot(canhit, transform.position);
    }
}
