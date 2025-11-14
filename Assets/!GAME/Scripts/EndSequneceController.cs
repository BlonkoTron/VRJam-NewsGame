using System.Collections;
using TMPro;
using UnityEngine;

public class EndSequneceController : MonoBehaviour
{

    [SerializeField] private GameObject EndCanvas;
    [SerializeField] private TMP_Text pointText;

    private float currentPoints = 0;
    private float pointTotal;
    [SerializeField] private float pointCountTime = 2f;



    private void Start()
    {
        pointTotal = PointManager.Instance.totalPoints;
        EndCanvas.SetActive(false);
    }

    public void StartEndScene()
    {
        EndCanvas.SetActive(true);

        StartCoroutine(pointAddingSequence(0, pointTotal, pointCountTime));

    }


    private IEnumerator pointAddingSequence(float from, float to, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration); // normalized [0,1]
            currentPoints = Mathf.Lerp(from, to, t);
            currentPoints = Mathf.RoundToInt(currentPoints);


            pointText.text = "Points: " + currentPoints.ToString();

            yield return null;
        }

        currentPoints = to; // ensure it ends exactly at target
        StopEndScene();
    }


    private void StopEndScene()
    {
        StopCoroutine(pointAddingSequence(0, pointTotal, pointCountTime));
    }
}
