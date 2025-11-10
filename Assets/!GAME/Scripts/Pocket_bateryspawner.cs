using UnityEngine;

public class Pocket_bateryspawner : MonoBehaviour
{
    public GameObject Battery;
    public Transform pocketposition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Spawnbattery();
    }

    public void Spawnbattery()
    {
        Instantiate(Battery, pocketposition.position, pocketposition.rotation);
    }
}
