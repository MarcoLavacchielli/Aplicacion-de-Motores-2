using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyFactory enemyFactory;
    [SerializeField] private List<Vector3> spawnPositions;

    private void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            Vector3 randomSpawnPosition = GetRandomSpawnPosition();
            IEnemy basicEnemy = enemyFactory.CreateBasicEnemy(randomSpawnPosition);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        int randomIndex = Random.Range(0, spawnPositions.Count);
        return spawnPositions[randomIndex];
    }
}
