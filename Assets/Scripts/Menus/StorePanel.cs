using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StorePanel : MonoBehaviour
{
    [SerializeField] private JsonSaveGameManager saveGameManager;
    [SerializeField] private int[] colorPrices = new int[3];
    [SerializeField] private GameObject[] comprarTexts = new GameObject[3];
    [SerializeField] private GameObject[] adquiridoTexts = new GameObject[3];
    [SerializeField] private GameObject[] activadoTexts = new GameObject[4];
    [SerializeField] private GameObject canvasStore;
    [SerializeField] private GameObject canvasMoney;

    void Awake()
    {
        saveGameManager = FindObjectOfType<JsonSaveGameManager>();

        if (saveGameManager == null)
        {
            Debug.LogError("JsonSaveGameManager no encontrado en la escena.");
        }
    }

    private void Update()
    {
        ButtonsManager();
        ActiveColor();
    }

    void Start()
    {
        foreach (var text in adquiridoTexts)
        {
            text.gameObject.SetActive(false);
        }
    }

    public void BuyColor(int colorIndex)
    {
        if (saveGameManager.saveData.currencyKey >= colorPrices[colorIndex])
        {
            saveGameManager.saveData.SetColorBought(colorIndex, true);
            saveGameManager.saveData.currencyKey -= colorPrices[colorIndex];
            saveGameManager.SaveGame();
        }
        else
        {
            ShowNoCurrencyPanel();
        }
    }

    public void ActivateColorClick(string colorKeymodified)
    {
        saveGameManager.saveData.ColorBullet = colorKeymodified;
        saveGameManager.SaveGame();
    }

    public void ButtonsManager()
    {
        for (int i = 0; i < comprarTexts.Length; i++)
        {
            if (saveGameManager.saveData.GetColorBought(i))
            {
                comprarTexts[i].SetActive(false);
                adquiridoTexts[i].SetActive(true);
            }
            else
            {
                comprarTexts[i].SetActive(true);
                adquiridoTexts[i].SetActive(false);
            }
        }
    }

    void ActiveColor()
    {
        string colorActivate = saveGameManager.saveData.ColorBullet;

        for (int i = 0; i < activadoTexts.Length; i++)
        {
            activadoTexts[i].gameObject.SetActive(false);
        }

        switch (colorActivate)
        {
            case "Blanco":
                activadoTexts[0].gameObject.SetActive(true);
                break;
            case "Rojo":
                activadoTexts[1].gameObject.SetActive(true);
                break;
            case "Azul":
                activadoTexts[2].gameObject.SetActive(true);
                break;
            case "Verde":
                activadoTexts[3].gameObject.SetActive(true);
                break;
            default:
                activadoTexts[0].gameObject.SetActive(true); // Activar "Blanco" por defecto
                break;
        }
    }

    public void ShowNoCurrencyPanel()
    {
        canvasStore.gameObject.SetActive(false);
        canvasMoney.gameObject.SetActive(true);
    }
}