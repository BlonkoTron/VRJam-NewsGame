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
    }
    public void Battery1_Enabled()
    {
        battery_1 = true;
    }

    public void Battery1_Disabled()
    {
        battery_1 = false;
    }

    public void Battery2_Enabled()
    {
        battery_2 = true;
    }

    public void Battery2_Disabled()
    {
        battery_1 = false;
    }
}
