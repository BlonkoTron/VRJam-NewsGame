using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.Video;

public class Introaudio : MonoBehaviour
{
    private EventInstance VoiceLine_1;

    [SerializeField] private EventReference AudioLine_1;

    public VideoPlayer vidplay;
    public bool halt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if (vidplay.isPlaying == true && halt == false)
        {
            VoiceLine_1 = Audiomanager.instance.PlaySound(AudioLine_1, transform.position);
            halt = true;
        }
        
    }

}
