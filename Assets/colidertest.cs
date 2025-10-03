using UnityEngine;

public class colidertest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("CUBE IS HERE");
    }
}
