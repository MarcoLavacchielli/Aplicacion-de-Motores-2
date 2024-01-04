using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*public interface IHealthObserver
{
    void OnHealthChanged(float health);
}*/

public class InterfazVida : MonoBehaviour
{
    [SerializeField] private Text textoVida;
    [SerializeField] private float vidaMaxima = 5f;
    private float vidaActual;

    void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarInterfazVida();

        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.OnHealthChange += OnHealthChanged;
        }
    }
    private void OnDestroy()
    {
        vidaActual = vidaMaxima;
        ActualizarInterfazVida();

        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.OnHealthChange -= OnHealthChanged;
        }
    }

    public void OnHealthChanged(float health)
    {
        vidaActual = health;
        ActualizarInterfazVida();
    }

    //Funcion para reducir?
    public void ReducirVida(float cantidad)
    {
        vidaActual -= cantidad;

        if (vidaActual <= 0)
        {
            vidaActual = 0;
        }

        ActualizarInterfazVida();
    }

    // Funcion para aumentar la vida del personaje
    public void AumentarVida(float cantidad)
    {
        vidaActual += cantidad;

        // Asegura que la vida no sea mayor que el máximo
        if (vidaActual > vidaMaxima)
        {
            vidaActual = vidaMaxima;
        }

        ActualizarInterfazVida();

    }

    // Actualizame UCHIIII
    private void ActualizarInterfazVida()
    {
        float porcentajeVida = vidaActual / vidaMaxima;
        textoVida.text = $"Vida: {vidaActual}/{vidaMaxima}"; // Actualiza el texto de vida
    }
}
