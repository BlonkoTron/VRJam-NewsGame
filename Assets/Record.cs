using UnityEngine;
using System.Collections.Generic;

public class HandheldCameraReplay : MonoBehaviour
{
    [Header("References")]
    public Transform player;       // your player transform
    public Transform handheldCam;  // the camera in the player's hand
    public Camera replayCam;       // the camera that outputs to a RenderTexture

    public ControllerButtons ControllerButtons;

    [Header("Settings")]
    public float recordDuration = 10f; // max recording length in seconds

    private List<FrameData> frames = new List<FrameData>();
    private int replayIndex = 0;
    private bool isRecording = false;
    private bool isReplaying = false;

    public bool block = false;

    // 👇 we define FrameData here inside the script
    [System.Serializable]
    public class FrameData
    {
        public Vector3 playerPos;
        public Quaternion playerRot;
        public Vector3 camPos;
        public Quaternion camRot;

        public FrameData(Transform player, Transform cam)
        {
            playerPos = player.position;
            playerRot = player.rotation;
            camPos = cam.position;
            camRot = cam.rotation;
        }
    }

    void Start()
    {
        ControllerButtons = FindFirstObjectByType<ControllerButtons>();
    }

    void Update()
    {
        if (ControllerButtons.newState == true) // start recording
        {
            isRecording = true;
            isReplaying = false;
            frames.Clear();
            Debug.Log("Started Recording");
        }

        if (ControllerButtons.newState == false) // stop & replay
        {
            isRecording = false;
            isReplaying = true;
            replayIndex = 0;
            Debug.Log("Started Replay");
        }

        if (isRecording)
        {
            Record();
        }
        else if (isReplaying)
        {
            PlayReplay();
        }
    }

    void Record()
    {
        frames.Add(new FrameData(player, handheldCam));

        // Keep only last X seconds
        int maxFrames = Mathf.RoundToInt(recordDuration / Time.deltaTime);
        if (frames.Count > maxFrames)
            frames.RemoveAt(0);
    }

    void PlayReplay()
    {
        if (replayIndex < frames.Count)
        {
            FrameData f = frames[replayIndex];

            // ⬇️ This overwrites the live player!
            // If you don’t want that, we’ll adjust next.
            player.position = f.playerPos;
            player.rotation = f.playerRot;

            handheldCam.position = f.camPos;
            handheldCam.rotation = f.camRot;

            replayIndex++;
        }
        else
        {
            isReplaying = false;
            Debug.Log("Replay finished");
        }
    }
}
