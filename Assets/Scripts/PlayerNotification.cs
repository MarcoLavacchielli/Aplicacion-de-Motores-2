using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerNotification : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.StartListening("EnemyDied", HandleEnemyDeath);
    }

    private void OnDisable()
    {
        EventManager.StopListening("EnemyDied", HandleEnemyDeath);
    }
    private void HandleEnemyDeath()
    {
        // Este método se ejecutará cada vez que un enemigo muera.
        // Puedes realizar acciones específicas relacionadas con el jugador aquí.
        NotifyPlayerObserverOfEnemyDeath();
    }
    private void NotifyPlayerObserverOfEnemyDeath()
    {
        PlayerObserver playerObserver = FindObjectOfType<PlayerObserver>();
        if (playerObserver != null)
        {
            playerObserver.HandleEnemyDeath();
        }
    }
}
