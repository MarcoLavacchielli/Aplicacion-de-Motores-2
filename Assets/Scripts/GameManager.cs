using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyFactory[] enemyFactories;
    [SerializeField] private List<Vector3> spawnPositions;
    [SerializeField] private float spawnDistanceThreshold = 10f;
    [SerializeField] private float timeBetweenSpawns = 2f;

    private PlayerController1 playerController;

    private Dictionary<int, EnemyStats> enemyStatsLookup = new Dictionary<int, EnemyStats>();

    [SerializeField] private ParticleSystem spawnParticle;

    [System.Serializable]
    public class EnemyStats
    {
        public int health;
        public int damage;
    }

    [SerializeField] private List<EnemyStats> enemyStatsList;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController1>();

        if (playerController == null)
        {
            Debug.LogError("PlayerController1 not found in the scene.");
        }

        // pre carga
        for (int i = 0; i < enemyStatsList.Count; i++)
        {
            enemyStatsLookup[i] = enemyStatsList[i];
        }

        StartCoroutine(SpawnEnemyWithDelay());
    }

    private void OnDrawGizmos()
    {
        if (playerController != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(playerController.transform.position, spawnDistanceThreshold);
        }
    }

    private IEnumerator SpawnEnemyWithDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            SpawnEnemy();
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
                int enemyType = Random.Range(0, enemyFactories.Length);

                if (enemyType >= 0 && enemyType < enemyFactories.Length)
                {
                    // Stats del look up
                    if (enemyStatsLookup.TryGetValue(enemyType, out EnemyStats enemyStats))
                    {
                        GameObject enemy = enemyFactories[enemyType].CreateEnemy(
                            randomSpawnPosition,
                            enemyStats.health,
                            enemyStats.damage
                        );

                        // Activar las partículas en la posición del enemigo
                        if (spawnParticle != null)
                        {
                            ParticleSystem spawnedParticles = Instantiate(spawnParticle, randomSpawnPosition, Quaternion.identity);
                            spawnedParticles.Play();
                            //Debug.Log("Particles spawned at position: " + randomSpawnPosition);
                        }
                        else
                        {
                            //Debug.LogError("Spawn Particle system not assigned to GameManager.");
                        }
                    }
                    else
                    {
                        Debug.LogError("Enemy stats not found for enemyType: " + enemyType);
                    }
                }
                else
                {
                    Debug.LogError("Invalid enemyType: " + enemyType);
                }
            }
        }
        else
        {
            Debug.LogWarning("PlayerController1 not found. Enemies will not be spawned.");
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        int randomIndex = Random.Range(0, spawnPositions.Count);
        return spawnPositions[randomIndex];
    }
}