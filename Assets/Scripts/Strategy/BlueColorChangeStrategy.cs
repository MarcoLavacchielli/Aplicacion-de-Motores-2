using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueColorChangeStrategy : IColorChangeStrategy
{
    public void ChangeColor(GameObject floor)
    {
        floor.GetComponent<Renderer>().material.color = Color.blue;
    }
}
