using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory1 : EnemyFactory
{
    [SerializeField] private GameObject enemyPrefab;

    public override GameObject CreateEnemy(Vector3 position, int health, int damage)
    {
        Enemy.Builder builder = new Enemy.Builder(enemyPrefab)
            .WithHealth(health)
            .WithDamage(damage);

        return builder.Build(position);
    }
}