using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JsonSaveGameKey
{
    //Key de la moneda
    public float currencyKey = 0;
    //Key nombre
    public string ColorBullet = "Blanco";
    //Key niveles
    public bool levelTutorialKey = true;
    public bool level1Key = false;
    public bool level2Key = false;
    //Colores comprados
    public bool colorRedBought = false;
    public bool colorBlueBought = false;
    public bool colorGreenBought = false;
    // Stamina
    public int stamine = 10;

    internal bool GetColorBought(int colorIndex)
    {
        switch (colorIndex)
        {
            case 0: return colorRedBought;
            case 1: return colorBlueBought;
            case 2: return colorGreenBought;
            default: return false; // Manejo de un índice incorrecto
        }
    }

    internal void SetColorBought(int colorIndex, bool value)
    {
        switch (colorIndex)
        {
            case 0: colorRedBought = value; break;
            case 1: colorBlueBought = value; break;
            case 2: colorGreenBought = value; break;
            // podemos agregar más casos si tenemos más colores a lo EA, sport tudigaime
            default: break; // Manejo de un índice incorrecto
        }
    }
}
