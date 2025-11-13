using UnityEngine;
using System.Collections;

public class ProtesterSpawner : MonoBehaviour
{
    
    [Header("Spawn Settings")]
    [SerializeField] private GameObject protesterAttackerPrefab;
    

    [Header("Timing (seconds)")]
    public float initialDelay = 5f;
    public float minSpawnInterval = 2f;
    public float maxSpawnInterval = 5f;

    private bool isSpawning = false;

    private void Awake()
    {
        isSpawning = false;
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {

        if (!isSpawning) 
        {
            isSpawning=true;
            yield return new WaitForSeconds(initialDelay);
        }

        while (isSpawning)
        {
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);

            SpawnPrefab();
        }
    }

    private void SpawnPrefab()
    {
        if (protesterAttackerPrefab != null)
        {
            Instantiate(protesterAttackerPrefab, transform.position, transform.rotation);
        }
    }





}
