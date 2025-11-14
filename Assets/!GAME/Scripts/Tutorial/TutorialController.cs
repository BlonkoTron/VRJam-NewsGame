using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    public static TutorialController Instance;

    public UnityEvent OnTutorialEnded;

    [SerializeField] private VideoPlayer video;
    [SerializeField] private GameObject playButton;

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
    public void Startvideo()
    {
        video.Play();
        video.loopPointReached += OnVideoEnd;
        playButton.SetActive(false);
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        video.loopPointReached -= OnVideoEnd;
        Debug.Log("video ended, now fade out and teleport player");
        OnTutorialEnded.Invoke();
        TransitionController.Instance.FadeOut();
        if (teleportDestination!=null)
        {
            TransitionController.Instance.OnFadeOutEnd.AddListener(TeleportPlayer);
        } else
        {
            TransitionController.Instance.OnFadeOutEnd.AddListener(NextScene);
        }
    }
    private void TeleportPlayer()
    {
        TransitionController.Instance.OnFadeOutEnd.RemoveListener(TeleportPlayer);
        GameObject player = FindAnyObjectByType<CharacterController>().gameObject;
        player.transform.position = teleportDestination.position;
        TransitionController.Instance.FadeIn(); 
    }
    private void NextScene()
    {
        TransitionController.Instance.OnFadeOutEnd.RemoveListener(NextScene);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCount > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
