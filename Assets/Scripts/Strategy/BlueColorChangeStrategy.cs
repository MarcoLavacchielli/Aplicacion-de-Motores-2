using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueColorChangeStrategy : IColorChangeStrategy
{
    public void ChangeColor(GameObject triggerObject)
    {
        triggerObject.GetComponent<Renderer>().material.color = Color.blue;
    }
}
