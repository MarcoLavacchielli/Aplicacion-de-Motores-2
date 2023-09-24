using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JsonSaveGameKey
{
    //Key de la moneda
    public float currencyKey = 0;
    //Key nombre
    public string playerNameKey = "MyName";
    //Key niveles
    public bool levelTutorialKey = true;
    public bool level1Key = false;
    public bool level2Key = false;
}
