using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyFactory[] enemyFactories;
    [SerializeField] private List<Vector3> spawnPositions;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, 3f);
    }

    private void SpawnEnemy()
    {
        Vector3 randomSpawnPosition = GetRandomSpawnPosition();
        int enemyType = Random.Range(1, 3);
        GameObject enemy = enemyFactories[enemyType].CreateEnemy(randomSpawnPosition, 100, 10);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        int randomIndex = Random.Range(0, spawnPositions.Count);
        return spawnPositions[randomIndex];
    }
}