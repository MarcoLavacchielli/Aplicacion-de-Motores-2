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
}
