using UnityEngine;

public class Batterydrain : MonoBehaviour
{

    public int BatteryLife;
    
    public bool BatteryDrain;
    public bool camstate;

    public Material DeadBattery;

    public ControllerButtons Controlbutton;

    private void Start()
    {
        Controlbutton = GameObject.Find("Right-Hand").GetComponent<ControllerButtons>();
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
    }

    public void NoDrain()
    {
        BatteryDrain = false;
    }

    public void BatteryDead()
    {
        gameObject.GetComponent<MeshRenderer>().material = DeadBattery;
    }
}
