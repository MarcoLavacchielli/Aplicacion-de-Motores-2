using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory1 : EnemyFactory
{
    [SerializeField] private GameObject enemyPrefab;

    public override GameObject CreateEnemy(Vector3 position, int health, int damage)
    {
        GameObject enemyObj = Instantiate(enemyPrefab, position, Quaternion.identity);
        var enemy = enemyObj.GetComponent<Enemy>();
        if (enemy != null)
        {
            return new Enemy.Builder(enemyObj)
                .WithHealth(health)
                .WithDamage(damage)
                .Build();
        }
        else
        {
            Debug.LogError("Failed to create Enemy: Enemy component not found.");
            return null;
        }
    }
}
