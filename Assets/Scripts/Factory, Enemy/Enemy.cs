using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    void TakeDamage(int amount);
    void Die();
}

public class Enemy : MonoBehaviour, IDamage
{
    [SerializeField] private int health;
    [SerializeField] private int damage;
    [SerializeField] private int enemyType; // 1 for Enemy, 2 for Enemy2

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        EventManager.TriggerEvent("EnemyDied");
        Destroy(gameObject);
    }

    // Builder pattern methods
    public class Builder
    {
        [SerializeField] private int health;
        private int damage;
        private int enemyType;
        private GameObject enemyPrefab;

        public Builder(GameObject prefab)
        {
            this.enemyPrefab = prefab;
        }

        public Builder WithHealth(int health)
        {
            this.health = health;
            return this;
        }

        public Builder WithDamage(int damage)
        {
            this.damage = damage;
            return this;
        }

        public Builder WithEnemyType(int enemyType)
        {
            this.enemyType = enemyType;
            return this;
        }

        public GameObject Build(Vector3 position)
        {
            GameObject enemyObj = Instantiate(enemyPrefab, position, Quaternion.identity);
            var enemy = enemyObj.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.health = health;
                enemy.damage = damage;
                enemy.enemyType = enemyType;
                return enemyObj;
            }
            else
            {
                Debug.LogError("Failed to create Enemy: Enemy component not found.");
                Destroy(enemyObj);
                return null;
            }
        }
    }
}