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
        // Suscribe el método OnSceneLoaded al evento SceneManager.sceneLoaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (keyToActivate == "1")
            {
                saveGameManager.saveData.level1Key = true;

                //Sumamos 100 pesos al terminar un nivel
                saveGameManager.saveData.currencyKey += 100;
            }
            else if (keyToActivate == "2")
            {
                saveGameManager.saveData.level2Key = true;

                //Sumamos 200 pesos al terminar un nivel
                saveGameManager.saveData.currencyKey += 200;
            }
            else
            {
                Debug.Log("no se escribio la key correcta");
            }

            SceneManager.LoadScene(sceneToGo);
        }
    }

    // Este método se llama cuando la escena se ha cargado
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("Checkeo");
        // Verifica si la escena cargada es la que especificaste en sceneToGo
        if (scene.name == sceneToGo)
        {
            // Encuentra el canvas en la escena cargada
            Canvas canvas = FindObjectOfType<Canvas>();

            // Encuentra los paneles que deseas activar/desactivar
            GameObject initialScreenPanel = canvas.transform.Find("Panel(InitialScreen)").gameObject;
            GameObject levelSelectorPanel = canvas.transform.Find("Panel(LevelSelector)").gameObject;

            // Desactiva el panel inicial y activa el panel del selector de niveles
            initialScreenPanel.SetActive(false);
            levelSelectorPanel.SetActive(true);
        }
    }
}