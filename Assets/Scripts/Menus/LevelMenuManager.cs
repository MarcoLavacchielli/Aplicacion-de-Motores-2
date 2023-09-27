using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject[] BotonesLevels = new GameObject[3];
    [SerializeField] private JsonSaveGameManager saveGameManager;
    [SerializeField] private Sprite botonDesactivadoSprite;
    private Sprite[] originalSprite;

    void Awake()
    {
        saveGameManager = FindObjectOfType<JsonSaveGameManager>();

        if (saveGameManager == null)
        {
            Debug.LogError("JsonSaveGameManager no encontrado en la escena.");
        }
    }

    private void Start()
    {
        originalSprite = new Sprite[BotonesLevels.Length];

        int index = 0;
        foreach (var boton in BotonesLevels)
        {
            originalSprite[index] = boton.GetComponent<Image>().sprite;
            index++;
        }
    }

    private void Update()
    {
        UpdateButton(0, saveGameManager.saveData.levelTutorialKey);
        UpdateButton(1, saveGameManager.saveData.level1Key);
        UpdateButton(2, saveGameManager.saveData.level2Key);
    }

    // Función genérica para actualizar botones
    private void UpdateButton(int index, bool isActive)
    {
        BotonesLevels[index].GetComponent<Button>().interactable = isActive;
        BotonesLevels[index].GetComponent<Image>().sprite = isActive ? originalSprite[index] : botonDesactivadoSprite;

        // Habilita o deshabilita el texto del botón en caso de tener uno
        TextMeshProUGUI textMeshPro = BotonesLevels[index].GetComponentInChildren<TextMeshProUGUI>();
        if (textMeshPro != null)
        {
            textMeshPro.enabled = isActive;
        }
    }
}