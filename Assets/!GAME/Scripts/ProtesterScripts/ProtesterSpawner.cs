using UnityEngine;
using System.Collections;

public class ProtesterSpawner : MonoBehaviour
{
    
    [Header("Spawn Settings")]
    [SerializeField] private GameObject protesterAttackerPrefab;
    

    [Header("Timing (seconds)")]
    public float minSpawnInterval = 2f;
    public float maxSpawnInterval = 5f;

    private bool isSpawning = true;

    private void Awake()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
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
        else
        {
            Debug.LogWarning("PrefabToSpawn not assigned!");
        }
    }





}
