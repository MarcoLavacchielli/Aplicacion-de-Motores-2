using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IColorChangeStrategy
{
    void ChangeColor(GameObject triggerObject);
}
public class RedColorChangeStrategy : IColorChangeStrategy
{
    public void ChangeColor(GameObject triggerObject)
    {
        triggerObject.GetComponent<Renderer>().material.color = Color.red;
    }
}
