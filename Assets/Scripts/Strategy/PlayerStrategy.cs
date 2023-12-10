using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrategy : MonoBehaviour
{
    private IColorChangeStrategy[] colorChangeStrategies;
    private int currentStrategyIndex;

    private void Start()
    {
        colorChangeStrategies = new IColorChangeStrategy[]
        {
            new RedColorChangeStrategy(),
            new BlueColorChangeStrategy()
        };

        currentStrategyIndex = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("TriggerObject"))
        {
            //currentStrategyIndex = (currentStrategyIndex + 1) % colorChangeStrategies.Length;

            //IColorChangeStrategy currentStrategy = colorChangeStrategies[currentStrategyIndex];
            //currentStrategy.ChangeColor(other.gameObject);
        }
    }
}