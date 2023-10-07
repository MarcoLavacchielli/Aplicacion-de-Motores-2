using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour, IEnemy
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
        private Enemy2 enemy;

        public Builder(GameObject enemyGameObject)
        {
            enemy = enemyGameObject.GetComponent<Enemy2>();
        }

        public Builder WithHealth(int health)
        {
            enemy.health = health;
            return this;
        }

        public Builder WithDamage(int damage)
        {
            enemy.damage = damage;
            return this;
        }

        public Builder WithEnemyType(int enemyType)
        {
            enemy.enemyType = enemyType;
            return this;
        }

        public GameObject Build()
        {
            return enemy.gameObject;
        }
    }
}