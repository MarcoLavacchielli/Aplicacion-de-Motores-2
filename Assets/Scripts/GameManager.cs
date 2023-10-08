using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyFactory[] enemyFactories;
    [SerializeField] private List<Vector3> spawnPositions;

    [SerializeField] private int healthEnemy1;
    [SerializeField] private int damageEnemy1;
    [SerializeField] private int healthEnemy2;
    [SerializeField] private int damageEnemy2;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, 3f);
    }

    private void SpawnEnemy()
    {
        Vector3 randomSpawnPosition = GetRandomSpawnPosition();
        int enemyType = Random.Range(1, 3);  // Generates 1 or 2
        enemyType--;  // Adjust to array indices (0 or 1)

        if (enemyType == 0 && enemyType < enemyFactories.Length)
        {
            // Spawn Enemy1
            GameObject enemy = enemyFactories[enemyType].CreateEnemy(randomSpawnPosition, healthEnemy1, damageEnemy1);
        }
        else if (enemyType == 1 && enemyType < enemyFactories.Length)
        {
            // Spawn Enemy2
            GameObject enemy2 = enemyFactories[enemyType].CreateEnemy(randomSpawnPosition, healthEnemy2, damageEnemy2);
        }
        else
        {
            Debug.LogError("Invalid enemyType: " + enemyType);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        int randomIndex = Random.Range(0, spawnPositions.Count);
        return spawnPositions[randomIndex];
    }
}