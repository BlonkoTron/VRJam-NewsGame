using UnityEngine;
using UnityEngine.Video;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;

[System.Serializable]
public class RankEntry
{
    public string rankName;
    public float pointThreshold;
    public VideoClip videoClip;

    [Header("FMOD Audio Event")]
    public EventReference fmodEvent;   // Updated from [EventRef] string

    [HideInInspector]
    public EventInstance fmodEventInstance;
}

public class RankSystems : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    [Header("Rank Settings")]
    public List<RankEntry> ranks = new List<RankEntry>();

    [Header("Point Manager")]
    public PointManager pointManager;

    [Header("Manual Testing")]
    public bool useManualPoints = false;
    public float manualPoints = 0f;

    public bool test;

    private RankEntry finalRank = null;
    private bool hasPlayed = false;

    void Start()
    {
        if (pointManager == null)
            pointManager = PointManager.Instance;

        // Sort ranks in ascending order by threshold
        ranks.Sort((a, b) => a.pointThreshold.CompareTo(b.pointThreshold));

        // Create FMOD instances for all ranks
        foreach (var rank in ranks)
        {
            if (!rank.fmodEvent.IsNull)
            {
                rank.fmodEventInstance = RuntimeManager.CreateInstance(rank.fmodEvent);
            }
        }

        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void Update()
    {
        if (test)
        {
            test = false;
            EvaluateRank();
            PlayFinalRank();
        }
    }

    public void EvaluateRank()
    {
        float points = useManualPoints ? manualPoints : pointManager.totalPoints;

        finalRank = ranks[0];

        foreach (var r in ranks)
        {
            if (points >= r.pointThreshold)
                finalRank = r;
        }
    }

    public void PlayFinalRank()
    {
        if (finalRank == null) return;
        hasPlayed = true;

        // --- PLAY VIDEO ---
        videoPlayer.Stop();
        videoPlayer.clip = finalRank.videoClip;
        videoPlayer.isLooping = false;
        videoPlayer.Play();

        // --- PLAY FMOD AUDIO ---
        if (finalRank.fmodEventInstance.isValid())
        {
            finalRank.fmodEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            finalRank.fmodEventInstance.start();
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        if (!hasPlayed) return;

        // Stop FMOD
        if (finalRank != null && finalRank.fmodEventInstance.isValid())
        {
            finalRank.fmodEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }

        hasPlayed = false;
    }
}
