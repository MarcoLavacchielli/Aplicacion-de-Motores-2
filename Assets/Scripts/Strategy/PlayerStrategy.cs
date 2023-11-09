using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrategy : MonoBehaviour
{
    public IColorChangeStrategy colorChangeStrategy;
    private void Start()
    {
        colorChangeStrategy = new RedColorChangeStrategy();
    }

    public void ChangeColorStrategy(IColorChangeStrategy newStrategy)
    {
        colorChangeStrategy = newStrategy;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (colorChangeStrategy != null && other.CompareTag("TriggerObject"))
        {
            colorChangeStrategy.ChangeColor(GameObject.FindWithTag("TriggerObject"));
        }
    }
}
