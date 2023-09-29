using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassLevel : MonoBehaviour
{
    [SerializeField] private JsonSaveGameManager saveGameManager;
    [SerializeField] private string keyToActivate;
    [SerializeField] private string sceneToGo;

    void Awake()
    {
        saveGameManager = FindObjectOfType<JsonSaveGameManager>();

        if (saveGameManager == null)
        {
            Debug.LogError("JsonSaveGameManager no encontrado en la escena.");
        }
    }

    private void Start()
    {
        // Llena el diccionario con las claves y valores correspondientes
        levelKeys["level1Key"] = saveGameManager.saveData.level1Key;
        levelKeys["level2Key"] = saveGameManager.saveData.level2Key;
        // Agrega más niveles según sea necesario
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && levelKeys.ContainsKey(keyToActivate))
        {
            // Activa la clave especificada en saveGameManager.saveData
            levelKeys[keyToActivate] = true;
            saveGameManager.SaveGame();
            SceneManager.LoadScene(sceneToGo);
        }
    }
}
