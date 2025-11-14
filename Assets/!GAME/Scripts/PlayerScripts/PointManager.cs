using System.Collections;
using TMPro;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public static PointManager Instance;

    public float totalPoints;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
       
        totalPoints = 0;

    }
    

}
