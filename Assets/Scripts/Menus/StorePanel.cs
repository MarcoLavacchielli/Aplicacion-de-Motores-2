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

    //Canvas de falta plata
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
            saveGameManager.SaveGame();
        }
        else
        {
            showNocurrencyPanel();
        }
    }

    public void BuycolorBlue()
    {
        if (saveGameManager.saveData.currencyKey >= colorBluePrice)
        {
            saveGameManager.saveData.colorBlueBought = true;
            saveGameManager.SaveGame();
        }
        else
        {
            showNocurrencyPanel();
        }
    }

    public void BuycolorGreen()
    {
        if (saveGameManager.saveData.currencyKey >= colorGreenPrice)
        {
            saveGameManager.saveData.colorGreenBought = true;
            saveGameManager.SaveGame();
        }
        else
        {
            showNocurrencyPanel();
        }
    }

    public void ActivateColorClick(string colorKeymodified)
    {
        saveGameManager.saveData.ColorBullet = colorKeymodified;
        saveGameManager.SaveGame();
    }

    void ButtonsManager()
    {
        //Blanco
        if (saveGameManager.saveData.ColorBullet != "Blanco")
        {
            whiteAdquiridoText.gameObject.SetActive(true);
        }

        //Rojo
        if(saveGameManager.saveData.colorRedBought == true)
        {
            redComprarText.gameObject.SetActive(false);
            redAdquiridoText.gameObject.SetActive(true); 
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
            blueAdquiridoText.gameObject.SetActive(true); 
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
            greenAdquiridoText.gameObject.SetActive(true); 
        }
        else if (saveGameManager.saveData.colorGreenBought == false)
        {
            greenComprarText.gameObject.SetActive(true);
            greenAdquiridoText.gameObject.SetActive(false);
        }
    }

    void ActiveColor()
    {
        string colorActivate = saveGameManager.saveData.ColorBullet;

        switch (colorActivate)
        {
            case "Blanco":
                whiteAdquiridoText.gameObject.SetActive(false);
                whiteACTIVADOText.gameObject.SetActive(true);

                //Desactivo los otros porque solo puede haber uno activado
                redACTIVADOText.gameObject.SetActive(false);
                blueACTIVADOText.gameObject.SetActive(false);
                greenACTIVADOText.gameObject.SetActive(false);
                break;

            case "Rojo":
                redAdquiridoText.gameObject.SetActive(false);
                redACTIVADOText.gameObject.SetActive(true);

                //Desactivo los otros porque solo puede haber uno activado
                whiteACTIVADOText.gameObject.SetActive(false);
                blueACTIVADOText.gameObject.SetActive(false);
                greenACTIVADOText.gameObject.SetActive(false);
                break;

            case "Azul":
                blueAdquiridoText.gameObject.SetActive(false);
                blueACTIVADOText.gameObject.SetActive(true);

                //Desactivo los otros porque solo puede haber uno activado
                whiteACTIVADOText.gameObject.SetActive(false);
                redACTIVADOText.gameObject.SetActive(false);
                greenACTIVADOText.gameObject.SetActive(false);
                break;

            case "Verde":
                greenAdquiridoText.gameObject.SetActive(false);
                greenACTIVADOText.gameObject.SetActive(true);

                //Desactivo los otros porque solo puede haber uno activado
                whiteACTIVADOText.gameObject.SetActive(false);
                redACTIVADOText.gameObject.SetActive(false);
                blueACTIVADOText.gameObject.SetActive(false);
                break;
            default:
                whiteAdquiridoText.gameObject.SetActive(false);
                whiteACTIVADOText.gameObject.SetActive(true);
                break;
        }
    }

    public void showNocurrencyPanel()
    {
        canvasStore.gameObject.SetActive(false);
        canvasMoney.gameObject.SetActive(true);
    }
}