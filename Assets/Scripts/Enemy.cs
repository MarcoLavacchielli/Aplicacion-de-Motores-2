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

    public event Action OnEnemyDeath;

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
}
public class Enemy2 : MonoBehaviour, IEnemy
{
    [SerializeField] private int health;
    [SerializeField] private int damage;

    public event Action OnEnemyDeath;

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
}
