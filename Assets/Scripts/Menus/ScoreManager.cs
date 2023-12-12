using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public List<DoorBuy> doors = new List<DoorBuy>();
    public PowerUpVelocity velocity = new PowerUpVelocity();
    public PowerUpHealth health = new PowerUpHealth();
    public PowerUpDamage damage = new PowerUpDamage();
    // Puedes usar doors para acceder a múltiples puertas.

    public void DoorBought(int index)
    {
        if (index >= 0 && index < doors.Count)
        {
            doors[index].Doorbuyed();
            AudioManager.Instance.PlaySFX(4);
        }
        else
        {
            Debug.LogError("Índice de puerta fuera de rango.");
        }
    }

    public void BuyVelocity()
    {
        velocity.PowerUpbuyed();
        AudioManager.Instance.PlaySFX(3);
    }

    public void BuyHealth()
    {
        health.PowerUpbuyed();
        AudioManager.Instance.PlaySFX(3);
    }

    public void BuyDamage()
    {
        damage.PowerUpbuyed();
        AudioManager.Instance.PlaySFX(3);
    }
}