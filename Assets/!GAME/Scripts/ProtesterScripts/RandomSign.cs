using UnityEngine;

public class RandomSign : MonoBehaviour
{

    [SerializeField] private Material[] mats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int randomSign = Random.Range(0, mats.Length);
        gameObject.GetComponent<Renderer>().material = mats[randomSign];
    }


}
