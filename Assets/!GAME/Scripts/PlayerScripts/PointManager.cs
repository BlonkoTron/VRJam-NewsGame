using UnityEngine;

public class PointManager : MonoBehaviour
{
    public static PointManager Instance;

    public float totalPoints;

    private void Awake()
    {
        Instance = this;
    }


}
