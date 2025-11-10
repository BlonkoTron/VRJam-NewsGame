using UnityEngine;

public class Batterydrain : MonoBehaviour
{
    public int BatteryLife;

    public Material Fullbattery;
    public Material DeadBattery;

    public ControllerButtons Controlbutton;
    public Pocket_bateryspawner PBattery;
    public BatteryState Batterstate;

    private MeshRenderer rendererInstance;

    private void Start()
    {
        // Cache references
        Controlbutton = GameObject.Find("Right-Hand").GetComponent<ControllerButtons>();
        PBattery = GameObject.Find("Batteryspawn").GetComponent<Pocket_bateryspawner>();
        Batterstate = GameObject.Find("Right-Hand").GetComponent<BatteryState>();

        // Get the MeshRenderer once
        rendererInstance = GetComponent<MeshRenderer>();

        // Give each battery its *own* unique material instance
        rendererInstance.material = new Material(Fullbattery);
    }

    private void Update()
    {
        if (Batterstate.battery_1 && Batterstate.battery_2)
        {
            if (Controlbutton.newState)
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
        // Assign a *new instance* of the dead material so this one is unique
        rendererInstance.material = new Material(DeadBattery);
        Controlbutton.recordIndicator.SetActive(false);
    }
}
