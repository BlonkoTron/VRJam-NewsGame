using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class Randomaudio : MonoBehaviour
{
    private EventInstance[] voiceLines = new EventInstance[5];

    [SerializeField] private EventReference AudioLine_1;
    [SerializeField] private EventReference AudioLine_2;
    [SerializeField] private EventReference AudioLine_3;
    [SerializeField] private EventReference AudioLine_4;
    [SerializeField] private EventReference AudioLine_5;

    public void Startrandom()
    {
        // Pick a random number between 0 and 4
        int randomIndex = Random.Range(0, 5);

        // Select the corresponding EventReference
        EventReference chosenEvent = randomIndex switch
        {
            0 => AudioLine_1,
            1 => AudioLine_2,
            2 => AudioLine_3,
            3 => AudioLine_4,
            _ => AudioLine_5,
        };

        // Play the selected sound and store the instance
        voiceLines[randomIndex] = Audiomanager.instance.PlaySound(chosenEvent, transform.position);
    }

    public void EndsSFX()
    {
        // Stop all currently playing sounds safely
        foreach (var voiceLine in voiceLines)
        {
            Audiomanager.instance.StopSound(voiceLine);
        }
    }
}
