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
        if (saveGameManager.saveData.stamine > 10)
        {
            saveGameManager.saveData.stamine = 10;
        }
        else if (saveGameManager.saveData.stamine < 0)
        {
            saveGameManager.saveData.stamine = 0;
        }
    }

    public void fillStamine()
    {
        saveGameManager.saveData.stamine += 10;
    }

}