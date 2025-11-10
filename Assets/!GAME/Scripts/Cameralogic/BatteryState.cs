using UnityEngine;

public class BatteryState : MonoBehaviour
{
    public bool battery_1;
    public bool battery_2;
    public bool Depleted;

    private void Start()
    {
        battery_1 = false;
        battery_1 = false;
        Depleted = false;
    }
    public void Battery1_Enabled()
    {
        if (Depleted == false)
        {
            battery_1 = true;
        }
        else if (Depleted == true)
        {
            battery_1 = false;
        }

    }

    public void Battery2_Enabled()
    {
        if (Depleted == false)
        {
            battery_2 = true;
        }
        else if (Depleted == true)
        {
            battery_2 = false;
        }
    }
}
