using UnityEngine;
using System.Collections;

public class BirdSpawner : MonoBehaviour
{
    [SerializeField] private GameObject birdPrefab;
    [SerializeField] private Vector3 spawnCubeSize = Vector3.one;

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
            isSpawning = true;
            yield return new WaitForSeconds(initialDelay);
        }

        while (isSpawning)
        {
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);

            SpawnBird();
        }
    }
    public void SpawnBird()
    {
        Vector3 spawnPos = new Vector3(transform.position.x + Random.Range(-spawnCubeSize.x / 2, spawnCubeSize.x / 2), transform.position.y + Random.Range(-spawnCubeSize.y / 2, spawnCubeSize.y / 2), transform.position.z + Random.Range(-spawnCubeSize.z / 2, spawnCubeSize.z / 2));
        GameObject bird = Instantiate(birdPrefab, spawnPos, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        // Set the color with custom alpha.
        Gizmos.color = new Color(0f, 1f, 0f, 0.5f); // Green with custom alpha

        // Draw the cube.
        Gizmos.DrawCube(transform.position, spawnCubeSize);

        // Draw a wire cube outline.
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, spawnCubeSize);
    }
}
