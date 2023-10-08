using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory2 : EnemyFactory
{
    [SerializeField] private GameObject enemy2Prefab;

    public override GameObject CreateEnemy(Vector3 position, int health, int damage)
    {
        Enemy2.Builder builder = new Enemy2.Builder(enemy2Prefab)
            .WithHealth(health)
            .WithDamage(damage);

        return builder.Build(position);
    }
}