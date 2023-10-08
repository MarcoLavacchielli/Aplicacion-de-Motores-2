using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory2 : EnemyFactory
{
    [SerializeField] private GameObject enemy2Prefab;

    public override GameObject CreateEnemy(Vector3 position, int health, int damage)
    {
        GameObject enemyObj2 = Instantiate(enemy2Prefab, position, Quaternion.identity);
        var enemy2 = enemyObj2.GetComponent<Enemy2>();
        if (enemy2 != null)
        {
            return new Enemy2.Builder(enemyObj2)
                .WithHealth(health)
                .WithDamage(damage)
                .Build();
        }
        else
        {
            Debug.LogError("Failed to create Enemy2: Enemy2 component not found.");
            return null;
        }
    }
}
