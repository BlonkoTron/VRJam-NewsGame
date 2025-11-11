using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
public class TransitionController : MonoBehaviour
{
    public static TransitionController Instance;
    [SerializeField] private float fadeTime = 1;
    [SerializeField] private AnimationCurve fadeCurve;
    [SerializeField] private Image fadeImage;

    public UnityEvent OnFadeInEnd;
    public UnityEvent OnFadeOutEnd;

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
    void Start()
    {
        StartCoroutine(FadeInCoroutine(fadeTime));
    }
    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine(fadeTime));
    }
    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine(fadeTime));
    }

    private IEnumerator FadeInCoroutine(float timer)
    {
        float journey = 0f;
        while (journey <= timer)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / timer);
            // fade in panel over time
            var fade = fadeCurve.Evaluate(1-percent);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fade);
            yield return null;
        }
        OnFadeInEnd.Invoke();
    }
    private IEnumerator FadeOutCoroutine(float timer)
    {
        float journey = 0f;
        while (journey <= timer)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / timer);
            // fade out panel over time
            var fade = fadeCurve.Evaluate(percent);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fade);
            yield return null;
        }
        OnFadeOutEnd.Invoke();
    }

}
