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
        private GameObject enemyGameObject;
        private Enemy enemy;

        public Builder(GameObject enemyGameObject)
        {
            this.enemyGameObject = enemyGameObject;
            enemy = enemyGameObject.GetComponent<Enemy>();
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
            return enemyGameObject;
        }
    }
}