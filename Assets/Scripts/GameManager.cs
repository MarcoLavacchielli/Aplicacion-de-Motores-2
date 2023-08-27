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
            int enemyType = Random.Range(1, 3);
            if(enemyType == 1)
            {
                IEnemy basicEnemy = enemyFactory.CreateEnemy(randomSpawnPosition);

            }
            else if(enemyType == 2)
            {
                IEnemy basicEnemy = enemyFactory.CreateEnemy2(randomSpawnPosition);
            }
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        int randomIndex = Random.Range(0, spawnPositions.Count);
        return spawnPositions[randomIndex];
    }
}
