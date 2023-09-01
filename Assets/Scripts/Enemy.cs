using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    event Action OnEnemyDeath;
    void TakeDamage(int amount);
    void Die();
}

public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] private int health;
    [SerializeField] private int damage;

    public event Action OnEnemyDeath = delegate { };

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
        OnEnemyDeath.Invoke();
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Aa");
            Die();
        }
    }
}

public class Enemy2 : MonoBehaviour, IEnemy
{
    [SerializeField] private int health;
    [SerializeField] private int damage;

    public event Action OnEnemyDeath = delegate { };

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
        OnEnemyDeath.Invoke();
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Aa");
            Die();
        }
    }
}
