using UnityEngine;

public class Batterydrain : MonoBehaviour
{

    public int BatteryLife;
    
    public bool BatteryDrain;
    public bool camstate;

    public Material DeadBattery;

    public ControllerButtons Controlbutton;
    public Pocket_bateryspawner PBattery;

    private void Start()
    {
        Controlbutton = GameObject.Find("Right-Hand").GetComponent<ControllerButtons>();
        PBattery = GameObject.Find("Batteryspawn").GetComponent<Pocket_bateryspawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BatteryDrain == true && Controlbutton.newState == true)
        {
            BatteryLife--;
        }

        if (BatteryLife <= 0)
        {
            BatteryLife = 0;
            BatteryDead();
        }
    }

    public void Drain()
    {
        BatteryDrain = true;
        Debug.Log("DRAIN");
    }

    public void NoDrain()
    {
        BatteryDrain = false;
        Debug.Log("NOOOO DRAIN");
    }

    public void BatteryDead()
    {
        gameObject.GetComponent<MeshRenderer>().material = DeadBattery;
        PBattery.Spawnbattery();

    }
}
