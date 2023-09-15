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
        GameObject enemy;

        if (enemyType == 1)
        {
            enemy = enemyFactory.CreateEnemy(randomSpawnPosition, 100, 10);
        }
        else
        {
            enemy = enemyFactory.CreateEnemy2(randomSpawnPosition, 150, 15);
        }

        // para agregarle algo a los enemigos que creas, no se si es buena idea hacer el disparo
    }

    private Vector3 GetRandomSpawnPosition()
    {
        int randomIndex = Random.Range(0, spawnPositions.Count);
        return spawnPositions[randomIndex];
    }
}