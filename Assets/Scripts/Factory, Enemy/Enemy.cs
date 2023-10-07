using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    void TakeDamage(int amount);
    void Die();
}

public class Enemy : MonoBehaviour, IEnemy
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

        public Builder(GameObject enemyGameObject)
        {
            this.enemyGameObject = enemyGameObject;
        }

        public Builder WithHealth(int health)
        {
            enemyGameObject.GetComponent<Enemy>().health = health;
            return this;
        }

        public Builder WithDamage(int damage)
        {
            enemyGameObject.GetComponent<Enemy>().damage = damage;
            return this;
        }

        public Builder WithEnemyType(int enemyType)
        {
            enemyGameObject.GetComponent<Enemy>().enemyType = enemyType;
            return this;
        }

        public GameObject Build()
        {
            return enemyGameObject;
        }
    }
}