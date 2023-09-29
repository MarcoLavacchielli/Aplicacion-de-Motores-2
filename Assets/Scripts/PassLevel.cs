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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (keyToActivate == "1")
            {
                saveGameManager.saveData.level1Key = true;
            }
            else if(keyToActivate == "2")
            {
                saveGameManager.saveData.level2Key = true;
            }
            else
            {
                Debug.Log("no se escribio la key correcta");
            }
            SceneManager.LoadScene(sceneToGo);
        }
    }
}
