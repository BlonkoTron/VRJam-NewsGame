using UnityEngine;

public class ProtesterWaypoints : MonoBehaviour
{
    public static ProtesterWaypoints Instance;

    public Transform[] waypoints;

    private void Awake()
    {
        Instance = this;
    }


}
