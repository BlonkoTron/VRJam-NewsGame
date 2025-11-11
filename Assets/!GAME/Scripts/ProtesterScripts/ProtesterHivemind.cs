using UnityEngine;

public class ProtesterHivemind : MonoBehaviour
{
    public static ProtesterHivemind Instance;

    public Transform[] waypoints;
    public Transform[] attackWaypoints;

    private void Awake()
    {
        Instance = this;
    }


}
