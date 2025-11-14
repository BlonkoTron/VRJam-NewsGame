using UnityEngine;

public class ProtesterHivemind : MonoBehaviour
{
    public static ProtesterHivemind Instance;

    [Header("Waypoints")]
    public Transform[] waypoints;
    public Transform[] attackWaypoints;

    [Header("Cosmetic Stuff")]
    public Material[] materials;

    [Header("ProtesterSpawnerStuff")]
    [SerializeField] private GameObject attackerSpawner;
    [SerializeField] private int despawnThreshold;

    private int deadProtesters;

    private void Awake()
    {
        Instance = this;
    }

    public void checkDeadProtesters()
    {
        deadProtesters++;

        if (deadProtesters >= despawnThreshold)
        {
            attackerSpawner.SetActive(false);
        }

    }
}
