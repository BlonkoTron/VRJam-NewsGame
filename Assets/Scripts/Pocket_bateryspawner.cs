using UnityEngine;

public class Pocket_bateryspawner : MonoBehaviour
{
    public GameObject Battery;
    public Transform pocketposition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Spawnbattery();
    }

    public void Spawnbattery()
    {
        GameObject Batter = Instantiate(Battery, pocketposition.position, pocketposition.rotation);
    }
}
