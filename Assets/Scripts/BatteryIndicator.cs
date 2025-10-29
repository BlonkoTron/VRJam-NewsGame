using System.Threading;
using UnityEngine;

public class BatteryIndicator : MonoBehaviour
{
    [Range(0, 100)]
    public int value;

    public Transform bar;
    public float maxWidth;

    private Vector3 initialScale;
    private Vector3 initialPos;

    public Batterydrain BatteryDrains;

    void Start()
    {
        initialScale = bar.localScale;
        initialPos = bar.localPosition;
        BatteryDrains = GameObject.Find("Battery").GetComponent<Batterydrain>();
    }

    void Update()
    {
        value = BatteryDrains.BatteryLife;

        float percent = Mathf.Clamp01(value / 100f);

        // Scale on z axis only
        bar.localScale = new Vector3(initialScale.x, initialScale.y, percent * initialScale.z);

        // Move so the left side stays fixed and the bar shrinks from right to left
        float offset = (initialScale.z - bar.localScale.z) * 0.5f;
        bar.localPosition = new Vector3(initialPos.x, initialPos.y + offset, initialPos.z);
    }
}
