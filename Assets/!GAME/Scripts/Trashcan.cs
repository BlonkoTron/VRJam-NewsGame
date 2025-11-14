using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Trashcan : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject trashThrowablePrefab;
    private GameObject myCan;
    [SerializeField] private float waveSpeed = 1;
    [SerializeField] private float waveAmount = 1;
    private float timeToRespawnCan = 5f;

    private void Start()
    {
        SpawnCan();
    }

    private void SpawnCan()
    {
        myCan = Instantiate(trashThrowablePrefab, spawnPoint.position,Quaternion.identity);
        myCan.GetComponent<ThrowableCan>().onGrabbed += OnMyCanGrabbed;
    }
    private void OnMyCanGrabbed()
    {
        myCan.GetComponent<ThrowableCan>().onGrabbed -= OnMyCanGrabbed;
        myCan = null;
        StartCoroutine(WaitToSPawnNewCan());

    }
    private IEnumerator WaitToSPawnNewCan()
    {
        yield return new WaitForSeconds(timeToRespawnCan);
        SpawnCan();
    }

}
