using UnityEngine;

public class SpecialPoints : MonoBehaviour
{
    public PointsRayCast pointsRayCast;


    void Awake()
    {
        pointsRayCast = GameObject.Find("BoxRayCast").GetComponent<PointsRayCast>();
    }

    void TargetHit(GameObject obj)
    {
        if (obj.CompareTag("OneTimePoints"))
        {
            obj.tag = "Untagged";
            pointsRayCast.currentPoints += 2500;
        }
    }




}
