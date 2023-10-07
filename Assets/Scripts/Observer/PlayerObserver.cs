using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObserver : MonoBehaviour
{
    private PlayerScore playerScore = new PlayerScore();

    private void Awake()
    {
        playerScore = FindObjectOfType<PlayerScore>();
    }

    public void HandleEnemyDeath()
    {
        // Maneja la notificaci�n de la muerte del enemigo aqu�.
        //Debug.Log("Un enemigo ha muerto.");
        playerScore.AddScore(100);
    }
}
