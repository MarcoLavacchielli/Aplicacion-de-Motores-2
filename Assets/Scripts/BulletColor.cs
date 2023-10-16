using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletColor : MonoBehaviour
{

    [SerializeField] private JsonSaveGameManager saveGameManager;
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

    void Update()
    {
        //Color de la bala -> Tienda temporal//
        CollorBullet();
    }

    public void CollorBullet()
    {
        Renderer renderer = GetComponent<Renderer>();

        switch (saveGameManager.saveData.ColorBullet)
        {
            case "Blanco":
                renderer.material = whiteMaterial;
                break;

            case "Rojo":
                renderer.material = redMaterial;
                break;

            case "Azul":
                renderer.material = blueMaterial;
                break;

            case "Verde":
                renderer.material = greenMaterial;
                break;
        }
    }

}
