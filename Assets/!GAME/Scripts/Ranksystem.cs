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

    public PointManager Pointmanagers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Pointmanagers = GameObject.Find("Pointssystem").GetComponent<PointManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Pointmanagers.totalPoints = 0;
    }
}
