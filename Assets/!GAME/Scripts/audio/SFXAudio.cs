using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class NPC_Soundmanager : MonoBehaviour
{
    private EventInstance VoiceLine_1;
    private EventInstance VoiceLine_2;
    private EventInstance VoiceLine_3;
    private EventInstance VoiceLine_4;
    private EventInstance VoiceLine_5;

    [SerializeField] private EventReference AudioLine_1;
    [SerializeField] private EventReference AudioLine_2;
    [SerializeField] private EventReference AudioLine_3;
    [SerializeField] private EventReference AudioLine_4;
    [SerializeField] private EventReference AudioLine_5;

    public bool test;

    public void storys()
    {
        VoiceLine_1 = Audiomanager.instance.PlaySound(AudioLine_1, transform.position);
    }

    public void Endstorys()
    {
        Audiomanager.instance.StopSound(VoiceLine_1);
        Audiomanager.instance.StopSound(VoiceLine_2);
        Audiomanager.instance.StopSound(VoiceLine_3);
        Audiomanager.instance.StopSound(VoiceLine_4);
        Audiomanager.instance.StopSound(VoiceLine_5);
    }
}
