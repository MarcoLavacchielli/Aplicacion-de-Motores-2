using UnityEngine;

public class JsonIndividualsSave : MonoBehaviour
{
    public JsonSaveGameManager saveGameManager;

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
        if (Input.GetKeyDown(KeyCode.U))
        {

        }
    }

    void Add100Currency()
    {
        saveGameManager.saveData.currencyKey += 100;
        // Llama a la función de guardar del JsonSaveGameManager
        saveGameManager.SaveGame();
    }

    void WhiteSkinActivate()
    {
        saveGameManager.saveData.ColorBullet = "Blanco";
        // Llama a la función de guardar del JsonSaveGameManager
        saveGameManager.SaveGame();
    }

    void BlueSkinActivate()
    {
        saveGameManager.saveData.ColorBullet = "Azul";
        // Llama a la función de guardar del JsonSaveGameManager
        saveGameManager.SaveGame();
    }

    void RojoSkinActivate()
    {
        saveGameManager.saveData.ColorBullet = "Rojo";
        // Llama a la función de guardar del JsonSaveGameManager
        saveGameManager.SaveGame();
    }

    void GreenSkinActivate()
    {
        saveGameManager.saveData.ColorBullet = "Azul";
        // Llama a la función de guardar del JsonSaveGameManager
        saveGameManager.SaveGame();
    }

    void Level1unlocked()
    {
        saveGameManager.saveData.level1Key = true;
        // Llama a la función de guardar del JsonSaveGameManager
        saveGameManager.SaveGame();
    }

    void Level2unlocked()
    {
        saveGameManager.saveData.level2Key = true;
        // Llama a la función de guardar del JsonSaveGameManager
        saveGameManager.SaveGame();
    }

}