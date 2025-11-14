using UnityEngine;
using UnityEngine.Video;
using System.Collections.Generic;

[System.Serializable]
public class RankEntry
{
    public string rankName;
    public float pointThreshold;
    public VideoClip videoClip;
}

public class RankSystems : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    [Header("Rank Settings")]
    public List<RankEntry> ranks = new List<RankEntry>();

    [Header("Point Manager (Real Game Points)")]
    public PointManager pointManager;

    [Header("Manual Testing")]
    public bool useManualPoints = false;
    public float manualPoints = 0f;

    [Header("Trigger")]
    public bool test;

    private VideoClip finalClip;
    private bool hasPlayed = false;

    void Start()
    {
        if (pointManager == null)
            pointManager = GameObject.Find("VideoPlayer").GetComponent<PointManager>();

        ranks.Sort((a, b) => a.pointThreshold.CompareTo(b.pointThreshold));

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

        finalClip = ranks[0].videoClip;

        foreach (var r in ranks)
        {
            if (points >= r.pointThreshold)
                finalClip = r.videoClip;
        }

        Debug.Log("Selected Rank Video = " + finalClip.name + " (points = " + points + ")");
    }

    public void PlayFinalRank()
    {
        hasPlayed = true;

        videoPlayer.Stop();
        videoPlayer.clip = finalClip;
        videoPlayer.playOnAwake = false;
        videoPlayer.isLooping = false;

        videoPlayer.Play();
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        if (!hasPlayed) return;

        vp.Stop();
        hasPlayed = false;
    }
}
