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

    [Range(1f, 50f)]
    [SerializeField] private float spawnDistanceThreshold = 10f; // Umbral de distancia para spawnear enemigos

    private PlayerController1 playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController1>();

        if (playerController == null)
        {
            Debug.LogError("PlayerController1 no encontrado en la escena.");
        }

        InvokeRepeating("SpawnEnemy", 0f, 2f);
    }

    private void OnDrawGizmos()
    {
        if (playerController != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(playerController.transform.position, spawnDistanceThreshold);
        }
    }

    private void SpawnEnemy()
    {
        if (playerController != null)
        {
            Vector3 randomSpawnPosition = GetRandomSpawnPosition();

            float distanceToPlayer = Vector3.Distance(randomSpawnPosition, playerController.transform.position);
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
        else
        {
            Debug.LogWarning("PlayerController1 no encontrado. No se spawnearán enemigos.");
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        int randomIndex = Random.Range(0, spawnPositions.Count);
        return spawnPositions[randomIndex];
    }
}