using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StorePanel : MonoBehaviour
{
    public JsonSaveGameManager saveGameManager;
    [SerializeField] private int colorRedPrice;
    [SerializeField] private int colorBluePrice;
    [SerializeField] private int colorGreenPrice;

    // Referencias a los TextMeshPro de "comprar" y "adquirido" para cada botón.
    [SerializeField] private GameObject whiteAdquiridoText;
    [SerializeField] private GameObject whiteACTIVADOText;
    [SerializeField] private GameObject redComprarText;
    [SerializeField] private GameObject redAdquiridoText;
    [SerializeField] private GameObject redACTIVADOText;
    [SerializeField] private GameObject blueComprarText;
    [SerializeField] private GameObject blueAdquiridoText;
    [SerializeField] private GameObject blueACTIVADOText;
    [SerializeField] private GameObject greenComprarText;
    [SerializeField] private GameObject greenAdquiridoText;
    [SerializeField] private GameObject greenACTIVADOText;

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
        TextTMPManager();
        ActiveColor();
    }

    void Start()
    {
        // Desactivar los textos "adquirido" al inicio.
        redAdquiridoText.gameObject.SetActive(false);
        blueAdquiridoText.gameObject.SetActive(false);
        greenAdquiridoText.gameObject.SetActive(false);
    }

    public void BuycolorRed()
    {
        if (saveGameManager.saveData.currencyKey >= colorRedPrice)
        {
            saveGameManager.saveData.colorRedBought = true;
        }
    }

    public void BuycolorBlue()
    {
        if (saveGameManager.saveData.currencyKey >= colorBluePrice)
        {
            saveGameManager.saveData.colorBlueBought = true;

            // Desactivar "comprar" y activar "adquirido" para el botón azul.
            blueComprarText.gameObject.SetActive(false);
            blueAdquiridoText.gameObject.SetActive(true);
        }
    }

    public void BuycolorGreen()
    {
        if (saveGameManager.saveData.currencyKey >= colorGreenPrice)
        {
            saveGameManager.saveData.colorGreenBought = true;

            // Desactivar "comprar" y activar "adquirido" para el botón verde.
            greenComprarText.gameObject.SetActive(false);
            greenAdquiridoText.gameObject.SetActive(true);
        }
    }

    void TextTMPManager()
    {
        //Rojo
        if(saveGameManager.saveData.colorRedBought == true)
        {
            redComprarText.gameObject.SetActive(false);
            redAdquiridoText.gameObject.SetActive(true); // Esto requiere un bool para activar
        }
        else if (saveGameManager.saveData.colorRedBought == false)
        {
            redComprarText.gameObject.SetActive(true);
            redAdquiridoText.gameObject.SetActive(false);
        }

        //Azul
        if (saveGameManager.saveData.colorBlueBought == true)
        {
            blueComprarText.gameObject.SetActive(false);
            blueAdquiridoText.gameObject.SetActive(true); // Esto requiere un bool para activar
        }
        else if (saveGameManager.saveData.colorBlueBought == false)
        {
            blueComprarText.gameObject.SetActive(true);
            blueAdquiridoText.gameObject.SetActive(false);
        }

        //Verde
        if (saveGameManager.saveData.colorGreenBought == true)
        {
            greenComprarText.gameObject.SetActive(false);
            greenAdquiridoText.gameObject.SetActive(true); // Esto requiere un bool para activar
        }
        else if (saveGameManager.saveData.colorGreenBought == false)
        {
            greenComprarText.gameObject.SetActive(true);
            greenAdquiridoText.gameObject.SetActive(false);
        }
    }

    void ActiveColor()
    {
        string colorActivate = saveGameManager.saveData.ColorBala;

        switch (colorActivate)
        {
            case "Blanco":
                break;

            case "Rojo":
                break;

            case "Azul":
                break;

            case "Verde":
                break;

            default:
                break;
        }
    }
}