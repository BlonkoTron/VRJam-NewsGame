using UnityEngine;
using UnityEngine.Video;

public class Ranksystem : MonoBehaviour
{
    public VideoPlayer Videoplayers;

    public VideoClip S_Rank;
    public VideoClip A_Rank;
    public VideoClip B_Rank;
    public VideoClip C_Rank;
    public VideoClip D_Rank;
    public VideoClip E_Rank;
    public VideoClip F_Rank;

    public bool S_rankCheck;
    public bool A_rankCheck;
    public bool B_rankCheck;
    public bool C_rankCheck;
    public bool D_rankCheck;
    public bool E_rankCheck;
    public bool F_rankCheck;

    public VideoClip FinalRank;
    public float S_Rank_PointThreshold;
    public float A_Rank_PointThreshold;
    public float B_Rank_PointThreshold;
    public float C_Rank_PointThreshold;
    public float D_Rank_PointThreshold;
    public float E_Rank_PointThreshold;
    public float F_Rank_PointThreshold;

    public PointManager Pointmanagers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Pointmanagers = GameObject.Find("Pointssystem").GetComponent<PointManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Pointmanagers.totalPoints <= F_Rank_PointThreshold && (F_rankCheck == false))
        {
            FinalRank = F_Rank;
            F_rankCheck = true;
        } else if (Pointmanagers.totalPoints <= E_Rank_PointThreshold && (E_rankCheck == false))
        {
            FinalRank = E_Rank;
            E_rankCheck = true;
        } else if (Pointmanagers.totalPoints <= D_Rank_PointThreshold && (D_rankCheck == false))
        {
            FinalRank = D_Rank;
            D_rankCheck = true;
        } else if (Pointmanagers.totalPoints <= C_Rank_PointThreshold && (C_rankCheck == false))
        {
            FinalRank = C_Rank;
            C_rankCheck = true;
        } else if (Pointmanagers.totalPoints <= B_Rank_PointThreshold && (B_rankCheck == false))
        {
            FinalRank = B_Rank;
            B_rankCheck = true;
        } else if (Pointmanagers.totalPoints <= A_Rank_PointThreshold && (A_rankCheck == false))
        {
            FinalRank = A_Rank;
            A_rankCheck = true;
        } else if (Pointmanagers.totalPoints <= S_Rank_PointThreshold && (S_rankCheck == false))
        {
            FinalRank = S_Rank;
            S_rankCheck = true;
        } 
    }

    public void FinalRankScore()
    {
        //here we play the current finalrank video when the game is over
        Videoplayers.clip = FinalRank;
    }
}
