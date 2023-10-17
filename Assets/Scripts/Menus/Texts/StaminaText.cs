using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StaminaText : MonoBehaviour
{
    [SerializeField] private JsonSaveGameManager saveGameManager;
    private TextMeshProUGUI textComponent;

    void Awake()
    {
        saveGameManager = FindObjectOfType<JsonSaveGameManager>();
        textComponent = GetComponent<TextMeshProUGUI>();

        if (saveGameManager == null)
        {
            Debug.LogError("JsonSaveGameManager no encontrado en la escena.");
        }

        if (textComponent == null)
        {
            Debug.LogError("Componente de texto no encontrado en el objeto.");
        }
    }

    void Update()
    {
        if (saveGameManager != null && saveGameManager.saveData != null && textComponent != null)
        {
            textComponent.text = "" + saveGameManager.saveData.stamine + "/10";
        }
    }
}
