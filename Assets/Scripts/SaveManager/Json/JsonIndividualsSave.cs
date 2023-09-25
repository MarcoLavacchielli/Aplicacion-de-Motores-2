using UnityEngine;

public class JsonIndividualsSave : MonoBehaviour
{
    public JsonSaveGameManager saveGameManager;

    void Start()
    {

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

    void WhiteSkin()
    {
        saveGameManager.saveData.ColorBala = "Blanco";
        // Llama a la función de guardar del JsonSaveGameManager
        saveGameManager.SaveGame();
    }

    void BlueSkin()
    {
        saveGameManager.saveData.ColorBala = "Azul";
        // Llama a la función de guardar del JsonSaveGameManager
        saveGameManager.SaveGame();
    }

    void RojoSkin()
    {
        saveGameManager.saveData.ColorBala = "Rojo";
        // Llama a la función de guardar del JsonSaveGameManager
        saveGameManager.SaveGame();
    }

    void GreenSkin()
    {
        saveGameManager.saveData.ColorBala = "Azul";
        // Llama a la función de guardar del JsonSaveGameManager
        saveGameManager.SaveGame();
    }

}