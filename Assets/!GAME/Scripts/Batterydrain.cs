using UnityEngine;

public class Batterydrain : MonoBehaviour
{

    public int BatteryLife;
   

    public Material DeadBattery;

    public ControllerButtons Controlbutton;
    public Pocket_bateryspawner PBattery;
    public BatteryState Batterstate;

    private void Start()
    {
        Controlbutton = GameObject.Find("Right-Hand").GetComponent<ControllerButtons>();
        PBattery = GameObject.Find("Batteryspawn").GetComponent<Pocket_bateryspawner>();
        Batterstate = GameObject.Find("Right-Hand").GetComponent<BatteryState>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( Batterstate.battery_1 == true && Batterstate.battery_2 == true)
        {
            if (Controlbutton.newState == true)
            {
                BatteryLife--;
            }

            if (BatteryLife <= 0)
            {
                BatteryLife = 0;
                BatteryDead();
            }
        }
    }

    public void BatteryDead()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material = new Material(DeadBattery);
    }

}
