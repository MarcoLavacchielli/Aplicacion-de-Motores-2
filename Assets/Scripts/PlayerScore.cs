using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int characterScore = 0;

    void Start()
    {
        // Puedes llamar a la función AddScore() en el inicio si deseas iniciar con una puntuación predeterminada.
        // AddScore();
    }

    public void AddScore(int amount)
    {
        // Incrementa la puntuación en 100 puntos.
        characterScore += amount;

        // Puedes agregar aquí cualquier lógica adicional relacionada con la puntuación.

        Debug.Log("Puntuación actual: " + characterScore);
    }

    public void SubstractScore(int amount)
    {
        characterScore -= amount;

        Debug.Log("Puntuación actual: " + characterScore);
    }
}
