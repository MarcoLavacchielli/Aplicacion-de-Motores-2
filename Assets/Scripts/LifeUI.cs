using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour, ILifeObserver
{
    private Text textComponent;

    private void Start()
    {
        textComponent = GetComponent<Text>();
    }

    public void OnLifeChanged(int newLife)
    {
        textComponent.text = "Vida: " + newLife.ToString();
    }
}
