using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using System.Collections;

public class Angrymob : MonoBehaviour
{
    private EventInstance Mobanngy;

    [SerializeField] private EventReference Mobangrier;
    [SerializeField] private float minTime = 10f;   // minimum delay
    [SerializeField] private float maxTime = 20f;   // maximum delay

    void Start()
    {
        StartCoroutine(SoundLoop());
    }

    IEnumerator SoundLoop()
    {
        while (true)
        {
            float waitTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(waitTime);

            sound();
        }
    }

    void sound()
    {
        Mobanngy = Audiomanmove.instance.PlaySound(Mobangrier, transform.position);
    }
}
