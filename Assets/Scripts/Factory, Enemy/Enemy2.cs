using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour, IDamage
{
    [SerializeField] private int health;
    [SerializeField] private int damage;
    [SerializeField] private int enemyType; // Cambiado a enemyType

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

    // Builder pattern methods en Enemy2
    public class Builder
    {
        private int health;
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
            var enemy2 = enemyObj.GetComponent<Enemy2>();
            if (enemy2 != null)
            {
                enemy2.health = health;
                enemy2.damage = damage;
                enemy2.enemyType = enemyType;
                return enemyObj;
            }
            else
            {
                Debug.LogError("Failed to create Enemy2: Enemy2 component not found.");
                Destroy(enemyObj);
                return null;
            }
        }
    }
}