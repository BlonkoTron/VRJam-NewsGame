using System.Collections;
using TMPro;
using UnityEngine;

public class EndSequneceController : MonoBehaviour
{

    [SerializeField] private GameObject EndCanvas;
    [SerializeField] private TMP_Text pointText;

    private bool startPoints = false;

    private float currentPoints = 0;
    private float pointTotal;
    [SerializeField] private float pointCountTime = 2f;
    [SerializeField] private float initialDelay = 6.2f;

    [Header("Rank Onjects")]
    private RankEntry pointRank;
    [SerializeField] private GameObject S_Rank;
    [SerializeField] private GameObject A_Rank;
    [SerializeField] private GameObject B_Rank;
    [SerializeField] private GameObject C_Rank;
    [SerializeField] private GameObject D_Rank;
    [SerializeField] private GameObject E_Rank;
    [SerializeField] private GameObject F_Rank;

    private void Start()
    {
        EndCanvas.SetActive(false);
    }

    public void StartEndScene(RankEntry rank)
    {
        pointTotal = PointManager.Instance.totalPoints;

        

        pointRank = rank;

        StartCoroutine(pointAddingSequence(0, pointTotal, pointCountTime));

    }


    private IEnumerator pointAddingSequence(float from, float to, float duration)
    {
        if (!startPoints)
        {
            startPoints = true;
            yield return new WaitForSeconds(initialDelay);

            EndCanvas.SetActive(true);

        }

        

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration); // normalized [0,1]
            currentPoints = Mathf.Lerp(from, to, t);
            currentPoints = Mathf.RoundToInt(currentPoints);

            pointText.text = currentPoints.ToString();

            yield return null;
        }

        currentPoints = to; // ensure it ends exactly at target

        RankShow(pointRank);

    }


    private void RankShow(RankEntry rank)
    {
        StopCoroutine(pointAddingSequence(0, pointTotal, pointCountTime));

        switch (pointRank.rankName)
        {
            case "S_rank":
                S_Rank.SetActive(true);
                return;

            case "A_rank":
                A_Rank.SetActive(true);
                return;

            case "B_rank":
                B_Rank.SetActive(true);
                return;

            case "C_rank":
                C_Rank.SetActive(true);
                return;

            case "D_rank":
                D_Rank.SetActive(true);
                return;

            case "E_rank":
                E_Rank.SetActive(true);
                return;

            case "F_rank":
                F_Rank.SetActive(true);
                return;

        }

    }
}
