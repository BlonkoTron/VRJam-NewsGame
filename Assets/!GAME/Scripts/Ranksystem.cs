using UnityEngine;
using UnityEngine.Video;

public class Ranksystem : MonoBehaviour
{
    public VideoPlayer Videoplayer;

    public VideoClip S_Rank;
    public VideoClip A_Rank;
    public VideoClip B_Rank;
    public VideoClip C_Rank;
    public VideoClip D_Rank;
    public VideoClip E_Rank;
    public VideoClip F_Rank;

    public VideoClip FinalRank;
    public float S_Rank_Threshold;
    public float A_Rank_Threshold;
    public float B_Rank_Threshold;
    public float C_Rank_Threshold;
    public float D_Rank_Threshold;
    public float E_Rank_Threshold;
    public float F_Rank_Threshold;

    public PointManager Pointmanagers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Pointmanagers = GameObject.Find("Pointssystem").GetComponent<PointManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Pointmanagers.totalPoints < F_Rank_Threshold)
        {
            FinalRank = F_Rank;
        } else if (Pointmanagers.totalPoints < E_Rank_Threshold)
        {
            FinalRank = E_Rank;
        } else if (Pointmanagers.totalPoints < D_Rank_Threshold)
        {
            FinalRank = D_Rank;
        } else if (Pointmanagers.totalPoints < C_Rank_Threshold)
        {
            FinalRank = C_Rank;
        } else if (Pointmanagers.totalPoints < B_Rank_Threshold)
        {
            FinalRank = B_Rank;
        } else if (Pointmanagers.totalPoints < A_Rank_Threshold)
        {
            FinalRank = A_Rank;
        } else if (Pointmanagers.totalPoints < S_Rank_Threshold)
        {
            FinalRank = S_Rank;
        } 
    }

    public void FinalRankScore()
    {
        //here we play the current finalrank video
    }
}
