using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
[System.Serializable]
public class DoorFlyweight
{
    public float radius = 5f; // Radio del área sensible.
    public LayerMask playerLayer; // La capa en la que se encuentra el jugador.
    public Material buyMaterial; // Material para cuando el jugador puede comprar.
    public Material notBuyMaterial; // Material para cuando el jugador no puede comprar.
}
*/

public class DoorBuy : MonoBehaviour
{
    [SerializeField] private int limit;
    public PlayerScore score;
    public GameObject panel; // Activa el botón de compra.
    [SerializeField] private int doorDataIndex;
    private Material originalMaterial;

    [SerializeField] private DoorFlyWeight doorFlyweight;
    private void Awake()
    {
        score = FindObjectOfType<PlayerScore>();
        originalMaterial = GetComponent<Renderer>().material; // Establece el material original una vez en Awake.
        score.OnScoreChanged += HandleScoreChanged;
    }
    private void HandleScoreChanged(int newScore)
    {
        Debug.Log("Puntaje del jugador ha cambiado a: " + newScore);
        CheckScoreForActions();

    }

    void CheckScoreForActions()
    {
        if (score.CharacterScore >= limit)
        {
            ActiveBuy();
        }
        else
        {
            DisableBuy();
        }
    }

    private void Update()
    {
        // Comprueba si hay colisión con el jugador dentro del radio.
        Collider[] colliders = Physics.OverlapSphere(transform.position, doorFlyweight.Radius, doorFlyweight.PlayerLayer);

        // Si hay colisión con el jugador, activa el booleano.
        bool playerInRange = colliders.Length > 0;

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
            renderer.material = doorFlyweight.BuyMaterial; // Asignar el nuevo material.
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
            renderer.material = doorFlyweight.NotBuyMaterial; // Asignar el nuevo material.
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
        //score.characterScore = score.characterScore - limit; // Resto el valor
        score.SubstractScore(limit);

        if (panel != null) // Desactivo el panel
        {
            panel.SetActive(false); // Activar el panel.
        }

        // Por ahora sin animaciones pero iría aquí
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Gizmo dibujado.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, doorFlyweight.Radius);
    }
}
