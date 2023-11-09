using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private float currentHealth;
    private float maxHealth = 5f;
    public event Action<float> OnHealthChange;

    [SerializeField] private ParticleSystem damageP;


    [SerializeField] private Checkpoint checkpoint;

    public float CurrentHealth
    {
        get { return currentHealth; }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        NotifyObservers();
    }

    /*private void Update()
    {
        if (maxHealth > 5)
        {
            maxHealth = 5;
        }
    }*/

    public void Health(float healthAmout)
    {
        currentHealth += healthAmout;

        if (currentHealth > 5)
        {
            currentHealth = 5;
        }

        NotifyObservers();
    }

    public void TakeDamage(float damageAmount)
    {
        if (checkpoint != null && checkpoint.IsCheckpointActivated())
        {
            // Reposicionar al jugador en el checkpoint y restablecer su vida
            Debug.Log("flag");
            transform.position = checkpoint.GetCheckpointPosition();
            currentHealth = maxHealth;
            NotifyObservers();
        }
        else
        {
            currentHealth -= damageAmount;
            damageP.Play();

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                StartCoroutine(RespawnCoroutine()); // Usaremos una corrutina para dar tiempo al jugador a reposicionarse antes de reiniciar la escena
            }
            else
            {
                NotifyObservers();
            }
        }
    }

    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(1f); // Espera 1 segundo antes de reiniciar la escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reiniciar la escena
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
