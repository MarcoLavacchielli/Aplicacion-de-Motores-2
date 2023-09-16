using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterfazVida : MonoBehaviour
{
    [SerializeField] private Text textoVida; // Referencia al texto de vida (Text)
    [SerializeField] private float vidaMaxima = 100f;
    private float vidaActual;

    void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarInterfazVida();
    }

    public void ReducirVida(float cantidad)
    {
        vidaActual -= cantidad;

        if (vidaActual <= 0)
        {
            vidaActual = 0;
            ReiniciarEscena(); // Llama a la función para reiniciar la escena
        }

        ActualizarInterfazVida();
    }

    // Otras funciones del script, como ActualizarInterfazVida, no se muestran aquí pero deben estar presentes.

    private void ReiniciarEscena()
    {
        // Carga la escena actual para reiniciar el juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // Función para aumentar la vida del personaje
    public void AumentarVida(float cantidad)
    {
        vidaActual += cantidad;

        // Asegura que la vida no sea mayor que el máximo
        if (vidaActual > vidaMaxima)
        {
            vidaActual = vidaMaxima;
        }

        ActualizarInterfazVida();

        // Aquí puedes agregar lógica adicional cuando el personaje gana vida.
    }

    // Actualiza la interfaz de vida con los valores actuales
    private void ActualizarInterfazVida()
    {
        float porcentajeVida = vidaActual / vidaMaxima;
        textoVida.text = $"Vida: {vidaActual}/{vidaMaxima}"; // Actualiza el texto de vida
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            float cantidadDeDano = 1f;
            ReducirVida(cantidadDeDano);
        }
    }
}
