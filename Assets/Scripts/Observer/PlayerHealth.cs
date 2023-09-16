using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private float currentHealth;
    private float maxHealth = 5f;
    private List<IHealthObserver> observers = new List<IHealthObserver>();

    public float CurrentHealth
    {
        get { return currentHealth; }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        NotifyObservers();
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

    public void AddObserver(IHealthObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IHealthObserver observer)
    {
        observers.Remove(observer);
    }

    private void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.OnHealthChanged(currentHealth);
        }
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
