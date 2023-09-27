using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int characterScore = 0;

    void Start()
    {
        // Puedes llamar a la funci�n AddScore() en el inicio si deseas iniciar con una puntuaci�n predeterminada.
        // AddScore();
    }

    public void AddScore(int amount)
    {
        // Incrementa la puntuaci�n en 100 puntos.
        characterScore += amount;

        // Puedes agregar aqu� cualquier l�gica adicional relacionada con la puntuaci�n.

        Debug.Log("Puntuaci�n actual: " + characterScore);
    }

    public void SubstractScore(int amount)
    {
        characterScore -= amount;

        Debug.Log("Puntuaci�n actual: " + characterScore);
    }
}
