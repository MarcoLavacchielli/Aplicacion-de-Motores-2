using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBuy : MonoBehaviour
{
    [SerializeField] private float radius = 5f; // Radio del área sensible. //
    [SerializeField] private LayerMask playerLayer; // La capa en la que se encuentra el jugador. //
    [SerializeField] private bool playerInRange = false; // El booleano que se activará cuando el jugador esté en el radio.
    [SerializeField] private int limit;

    public PlayerScore score;

    [SerializeField] private Material BuynewMaterial; //
    [SerializeField] private Material NOTnewMaterial; //
    private Material originalMaterial;

    [SerializeField] private GameObject panel; //Activa el boton de compra.

    private void Awake()
    {
        score = FindObjectOfType<PlayerScore>();
        originalMaterial = GetComponent<Renderer>().material; // Establece el material original una vez en Awake.
    }

    private void Update()
    {
        // Comprueba si hay colisión con el jugador dentro del radio.
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, playerLayer);

        // Si hay colisión con el jugador, activa el booleano.
        playerInRange = colliders.Length > 0;

        // Realiza aquí las acciones que desees cuando el jugador esté en el radio.
        if (playerInRange)
        {
            if (score.characterScore >= limit)
            {
                ActiveBuy();
            }
            else
            {
                DisableBuy();
            }
        }
        else
        {
            PlayerAway();
        }
    }

    void ActiveBuy()
    {
        Renderer renderer = GetComponent<Renderer>(); // Obtener el componente Renderer del objeto.
        if (renderer != null)
        {
            renderer.material = BuynewMaterial; // Asignar el nuevo material.
        }

        if (panel != null)
        {
            panel.SetActive(true); // Activar el panel.
        }
    }

    void DisableBuy()
    {
        Renderer renderer = GetComponent<Renderer>(); // Obtener el componente Renderer del objeto.
        if (renderer != null)
        {
            renderer.material = NOTnewMaterial; // Asignar el nuevo material.
        }

        if (panel != null)
        {
            panel.SetActive(false); // Desactiva el panel.
        }
    }

    void PlayerAway()
    {
        Renderer renderer = GetComponent<Renderer>(); // Obtener el componente Renderer del objeto.
        if (renderer != null)
        {
            renderer.material = originalMaterial; // Reasignar el material original cuando el jugador se aleja del rango.
        }

        if (panel != null)
        {
            panel.SetActive(false); // Desactiva el panel.
        }
    }

    public void Doorbuyed()
    {
        score.characterScore = score.characterScore - limit; //Resto el valor

        if (panel != null) //Desactivo el panel
        {
            panel.SetActive(false); // Activar el panel.
        }

        //Por ahora sin animaciones pero iria aca
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Giazmo dibujado.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}