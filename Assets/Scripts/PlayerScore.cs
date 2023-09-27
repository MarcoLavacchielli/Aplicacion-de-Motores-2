using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    public int characterScore = 0;

    public int CharacterScore
    {
        get { return characterScore; }
        set
        {
            characterScore = value;
            UpdateScoreText();
            NotifyScoreChanged();
        }
    }

    public delegate void ScoreChangedEventHandler(int newScore);
    public event ScoreChangedEventHandler OnScoreChanged;

    public void Update()
    {
        UpdateScoreText();
    }
    public void AddScore(int amount)
    {
        // Incrementa la puntuación en 100 puntos.
        characterScore += amount;

        Debug.Log("Puntuación actual: " + characterScore);
    }

    public void SubstractScore(int amount)
    {
        characterScore -= amount;

        Debug.Log("Puntuación actual: " + characterScore);
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + characterScore;
        }
    }
    private void NotifyScoreChanged()
    {
        if (OnScoreChanged != null)
        {
            OnScoreChanged(characterScore);
        }
    }
}
