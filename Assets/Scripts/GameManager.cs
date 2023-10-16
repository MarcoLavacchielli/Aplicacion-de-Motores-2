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

    [SerializeField] private float spawnDistanceThreshold = 10f; // Umbral de distancia para spawnear enemigos

    private Transform player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController1>().transform;

        InvokeRepeating("SpawnEnemy", 0f, 3f);
    }

    private void SpawnEnemy()
    {
        Vector3 randomSpawnPosition = GetRandomSpawnPosition();

        float distanceToPlayer = Vector3.Distance(randomSpawnPosition, player.position);
        if (distanceToPlayer < spawnDistanceThreshold)
        {
            int enemyType = Random.Range(1, 3);
            enemyType--;

            if (enemyType >= 0 && enemyType < enemyFactories.Length)
            {
                GameObject enemy = enemyFactories[enemyType].CreateEnemy(
                    randomSpawnPosition,
                    (enemyType == 0) ? healthEnemy1 : healthEnemy2,
                    (enemyType == 0) ? damageEnemy1 : damageEnemy2
                );
            }
            else
            {
                Debug.LogError("Invalid enemyType: " + enemyType);
            }
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        int randomIndex = Random.Range(0, spawnPositions.Count);
        return spawnPositions[randomIndex];
    }
}