using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Video;

public class TutorialController : MonoBehaviour
{
    public static TutorialController Instance;

    public UnityEvent OnTutorialEnded;

    [SerializeField] private VideoPlayer video;

    [SerializeField] private Transform teleportDestination;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        video.loopPointReached += OnVideoEnd;
    }
    private void OnDestroy()
    {
        video.loopPointReached -= OnVideoEnd;
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        Debug.Log("video ended, now teleport player");
        OnTutorialEnded.Invoke();
        TransitionController.Instance.FadeOut();
        TransitionController.Instance.OnFadeOutEnd.AddListener(TeleportPlayer);
        TeleportPlayer();
    }
    private void TeleportPlayer()
    {
        TransitionController.Instance.OnFadeOutEnd.RemoveListener(TeleportPlayer);
        GameObject player = FindAnyObjectByType<CharacterController>().gameObject;
        player.transform.position = teleportDestination.position;
    }
}
