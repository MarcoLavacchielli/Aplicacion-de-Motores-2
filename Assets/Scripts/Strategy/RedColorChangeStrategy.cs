using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IColorChangeStrategy
{
    void ChangeColor(GameObject floor);
}
public class RedColorChangeStrategy : IColorChangeStrategy
{
    public void ChangeColor(GameObject floor)
    {
        floor.GetComponent<Renderer>().material.color = Color.red;
    }
}
