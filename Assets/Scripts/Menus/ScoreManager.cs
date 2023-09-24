using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public List<DoorBuy> doors = new List<DoorBuy>();

    // Puedes usar doors para acceder a m�ltiples puertas.

    public void DoorBought(int index)
    {
        if (index >= 0 && index < doors.Count)
        {
            doors[index].Doorbuyed();
        }
        else
        {
            Debug.LogError("�ndice de puerta fuera de rango.");
        }
    }
}
