using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [Header("Spawning Settings")]
    [SerializeField] private GameObject pipePrefab;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject obstaclePrefab;

    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float spawnX = 10f;
    [SerializeField] private float minY = -2f;
    [SerializeField] private float maxY = 2f;

    [Header("Obstacle Settings")]
    [SerializeField] private float obstacleSpawnChance = 0.4f; // 40%
    [SerializeField] private float obstacleOffsetY = 1.2f;     // distance from pipe

    [Header("Power Up Settings")]
    [SerializeField] private GameObject invisibilityPowerUpPrefab;
    [SerializeField] private float powerUpSpawnChance = 0.1f;  // 10% chance

    private float spawnTimer = 0f;
    private bool canSpawn = true;

    void Update()
    {
        if (!canSpawn) return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnRate)
        {
            SpawnPipe();
            spawnTimer = 0f;
        }
    }

    void SpawnPipe()
    {
        // random vertical position
        float randomY = UnityEngine.Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(spawnX, randomY, 0);

        // spawn the pipe pair
        GameObject pipe = Instantiate(pipePrefab, spawnPos, Quaternion.identity);

        // coin spawn point inside pipe prefab
        Transform coinPoint = pipe.transform.Find("CoinSpawnPoint");

        // ============================
        //        SPAWN COIN
        // ============================
        if (coinPoint != null && coinPrefab != null)
        {
            Instantiate(coinPrefab, coinPoint.position, Quaternion.identity, pipe.transform);
        }

        // =====================================
        //     SPAWN INVISIBILITY POWER-UP
        // =====================================
        if (invisibilityPowerUpPrefab != null && UnityEngine.Random.value < powerUpSpawnChance)
        {
            Vector3 powerUpPos = coinPoint != null
                ? coinPoint.position + new Vector3(0, 1f, 0)  // spawn above coin
                : spawnPos + new Vector3(0, 1f, 0);           // fallback pos

            Instantiate(invisibilityPowerUpPrefab, powerUpPos, Quaternion.identity, pipe.transform);
        }

        // =====================================
        //        RANDOM OBSTACLE SPAWN
        // =====================================
        if (obstaclePrefab != null && UnityEngine.Random.value < obstacleSpawnChance)
        {
            bool spawnTop = UnityEngine.Random.value > 0.5f;

            Transform topPipe = pipe.transform.Find("TopPipe");
            Transform bottomPipe = pipe.transform.Find("BottomPipe");

            if (spawnTop && topPipe != null)
            {
                Vector3 pos = topPipe.position + new Vector3(0, obstacleOffsetY, 0);
                Instantiate(obstaclePrefab, pos, Quaternion.identity, pipe.transform);
            }
            else if (!spawnTop && bottomPipe != null)
            {
                Vector3 pos = bottomPipe.position - new Vector3(0, obstacleOffsetY, 0);
                Instantiate(obstaclePrefab, pos, Quaternion.identity, pipe.transform);
            }
        }
    }

    public void StopSpawning()
    {
        canSpawn = false;
    }

    public void StartSpawning()
    {
        canSpawn = true;
        spawnTimer = 0f;
    }
}
