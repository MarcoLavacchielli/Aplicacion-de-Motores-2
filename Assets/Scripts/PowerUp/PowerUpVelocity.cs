using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpVelocity : MonoBehaviour
{
    [SerializeField] private int radius;
    [SerializeField] private int limit;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] private int PowerUpDataIndex;
    [SerializeField] private bool bought;
    public PlayerScore scoreVelocity;
    public PlayerModel pjMovement;
    [SerializeField] private GameObject panel; // Activa el botón de compra.
    [SerializeField] private GameObject velocityImage;

    [SerializeField] ParticleSystem powerUpBuyP;

    private void Awake()
    {
        scoreVelocity = FindObjectOfType<PlayerScore>();
        scoreVelocity.OnScoreChanged += HandleScoreChanged;
        pjMovement = FindObjectOfType<PlayerModel>();
        bought = false;
    }
    private void HandleScoreChanged(int newScore)
    {
        Debug.Log("Puntaje del jugador ha cambiado a: " + newScore);

        CheckScoreForActions();

    }

    private void Update()
    {
        // Comprueba si hay colisión con el jugador dentro del radio.
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, playerLayer);

        // Si hay colisión con el jugador, activa el booleano.
        bool playerInRange = colliders.Length > 0;
        if (playerInRange)
        {
            if (scoreVelocity.characterScore >= limit && bought == false)
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

    void CheckScoreForActions()
    {
        if (scoreVelocity.CharacterScore >= limit)
        {
            ActiveBuy();
        }
        else
        {
            DisableBuy();
        }
    }

    void ActiveBuy()
    {
        Renderer renderer = GetComponent<Renderer>(); 
        if (renderer != null)
        {
            //renderer.material = doorFlyweight.BuyMaterial; // Asignar el nuevo material.
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
            //renderer.material = doorFlyweight.NotBuyMaterial; // Asignar el nuevo material.
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
            //renderer.material = originalMaterial; // Reasignar el material original cuando el jugador se aleja del rango.
        }

        if (panel != null)
        {
            panel.SetActive(false); // Desactiva el panel.
        }
    }

    public void PowerUpbuyed()
    {
        //score.characterScore = score.characterScore - limit; // Resto el valor
        scoreVelocity.SubstractScore(limit);

        powerUpBuyP.Play();

        if (panel != null) // Desactivo el panel
        {
            panel.SetActive(false); // Activar el panel.
        }

        // Por ahora sin animaciones pero iría aquí
        //Destroy(gameObject);
        pjMovement.playerSpeed = pjMovement.playerSpeed + 2;
        bought = true;
        velocityImage.SetActive(true);
    }

    private void OnDrawGizmosSelected()
    {
        // Gizmo dibujado.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
