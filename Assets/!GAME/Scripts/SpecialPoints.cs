using UnityEngine;

public class SpecialPoints : MonoBehaviour
{
    void TargetHit(GameObject obj)
    {
        if (obj.CompareTag("OneTimePoints"))
        {
            obj.tag = "Untagged";
            PointManager.Instance.totalPoints += 2500;
        }
    }
}
