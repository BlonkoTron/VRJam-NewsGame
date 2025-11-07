using System.Collections.Generic;
using UnityEngine;

public class AnimationSequenceManager : MonoBehaviour
{
    public static AnimationSequenceManager m_SequenceManager;

    [SerializeField] private List<GameObject> animationObjects;

    private void Awake()
    {
        foreach (GameObject obj in animationObjects)
        {
            obj.SetActive(false);
        }
    }

    public void StartAnimation(int sequence)
    {
        animationObjects[sequence].SetActive(true);
    }



}
