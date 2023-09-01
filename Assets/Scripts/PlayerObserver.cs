using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObserver : MonoBehaviour
{
    private void Start()
    {
        MonoBehaviour[] monoBehaviours = FindObjectsOfType<MonoBehaviour>();
        foreach (var monoBehaviour in monoBehaviours)
        {
            if (monoBehaviour is IEnemy enemy)
            {
                enemy.OnEnemyDeath += HandleEnemyDeath;
            }
        }
    }
    private void HandleEnemyDeath()
    {
        // Maneja la notificación de la muerte del enemigo aquí.
        Debug.Log("Un enemigo ha muerto.");
    }
}
