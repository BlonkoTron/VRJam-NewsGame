using System.Collections.Generic;
using UnityEngine;

public class AnimationSequenceManager : MonoBehaviour
{
    public static AnimationSequenceManager Instance;

    [SerializeField] private List<GameObject> animationObjects;

    private void Awake()
    {

        Instance = this;

        foreach (GameObject obj in animationObjects)
        {
            obj.SetActive(false);
        }

        StartAnimation(0);

    }

    public void StartAnimation(int sequence)
    {
        animationObjects[sequence].SetActive(true);
    }



}
