using UnityEngine;

public class ProtesterHivemind : MonoBehaviour
{
    public static ProtesterHivemind Instance;

    [Header("Waypoints")]
    public Transform[] waypoints;
    public Transform[] attackWaypoints;

    [Header("Cosmetic Stuff")]
    public Material[] materials;

    private void Awake()
    {
        Instance = this;
    }


}
