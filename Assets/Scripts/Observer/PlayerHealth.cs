using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private float currentHealth;
    private float maxHealth = 5f;
    public event Action<float> OnHealthChange;

    public float CurrentHealth
    {
        get { return currentHealth; }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        NotifyObservers();
    }

    private void Update()
    {
        if (maxHealth > 5)
        {
            maxHealth = 5;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reiniciar la escena
        }

        NotifyObservers();
    }

    private void NotifyObservers()
    {
        OnHealthChange?.Invoke(currentHealth);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            float damageAmount = 1f;
            TakeDamage(damageAmount);
        }
    }
}
