using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    public int characterScore = 0;

    void Update()
    {
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        // Incrementa la puntuaci�n en 100 puntos.
        characterScore += amount;

        Debug.Log("Puntuaci�n actual: " + characterScore);
    }

    public void SubstractScore(int amount)
    {
        characterScore -= amount;

        Debug.Log("Puntuaci�n actual: " + characterScore);
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + characterScore;
        }
    }
}
