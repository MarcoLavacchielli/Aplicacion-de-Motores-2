using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyFactory enemyFactory;
    [SerializeField] private List<Vector3> spawnPositions;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, 3f);
    }

    private void SpawnEnemy()
    {
        Vector3 randomSpawnPosition = GetRandomSpawnPosition();
        int enemyType = Random.Range(1, 3);
        IEnemy basicEnemy;

        if (enemyType == 1)
        {
            basicEnemy = enemyFactory.CreateEnemy(randomSpawnPosition);
        }
        else
        {
            basicEnemy = enemyFactory.CreateEnemy2(randomSpawnPosition);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        int randomIndex = Random.Range(0, spawnPositions.Count);
        return spawnPositions[randomIndex];
    }
}
