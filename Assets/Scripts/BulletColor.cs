using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IBulletColorDecorator
{
    void ApplyColorToBullet(GameObject bullet);
}
public class BulletColor : MonoBehaviour
{
    [SerializeField] private JsonSaveGameManager saveGameManager;
    [SerializeField] private IBulletColorDecorator bulletColorDecorator;
    [SerializeField] private Material whiteMaterial;
    [SerializeField] private Material redMaterial;
    [SerializeField] private Material blueMaterial;
    [SerializeField] private Material greenMaterial;

    void Awake()
    {
        saveGameManager = FindObjectOfType<JsonSaveGameManager>();

        if (saveGameManager == null)
        {
            Debug.LogError("JsonSaveGameManager no encontrado en la escena.");
        }
    }

    void Start()
    {
        // Crea el decorador según el color guardado
        switch (saveGameManager.saveData.ColorBullet)
        {
            case "Blanco":
                bulletColorDecorator = new BulletColorDecorator(whiteMaterial);
                break;
            case "Rojo":
                bulletColorDecorator = new BulletColorDecorator(redMaterial);
                break;
            case "Azul":
                bulletColorDecorator = new BulletColorDecorator(blueMaterial);
                break;
            case "Verde":
                bulletColorDecorator = new BulletColorDecorator(greenMaterial);
                break;
            default:
                // Color por defecto
                bulletColorDecorator = new BulletColorDecorator(whiteMaterial);
                break;
        }

        // Aplica el color a la bala usando el decorador
        bulletColorDecorator.ApplyColorToBullet(gameObject);
    }
}
