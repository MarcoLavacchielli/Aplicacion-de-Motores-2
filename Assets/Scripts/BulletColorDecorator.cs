using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletColorDecorator : IBulletColorDecorator
{
    private Material material;

    public BulletColorDecorator(Material material)
    {
        this.material = material;
    }

    public void ApplyColorToBullet(GameObject bullet)
    {
        Renderer renderer = bullet.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = material;
        }
    }
}
