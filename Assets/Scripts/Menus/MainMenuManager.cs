using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    /*[SerializeField] private GameObject initialScreenPanel;
    [SerializeField] private GameObject titleScreenPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject levelSelectorPanel;
    [SerializeField] private GameObject storePanel;*/

    [SerializeField] private GameObject[] panels;

    [SerializeField] private JsonSaveGameManager saveGameManager;
    [SerializeField] private GameObject imageStamina;
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private  float intensityShake = 0.1f;

    void Awake()
    {
        saveGameManager = FindObjectOfType<JsonSaveGameManager>();
    }

    public void SwitchPanel(int index)  //Setea por indice, el panel a activar
    {
        if (index >= 0 && index < panels.Length)
        {
            panels[index].SetActive(!panels[index].activeSelf);
            AudioManager.Instance.PlaySFX(0); //hace Play del sonido del boton (actualmente, indice 0)
        }
    }

    public void TogglePanel(GameObject panelToToggle) //setea por editor, el panel a desactivar
    {
        panelToToggle.SetActive(!panelToToggle.activeSelf);
    }

    public void loadscene(string scenename) //carga una escena especifica por nombre
    {
        SceneManager.LoadScene(scenename);    //sin estamina
    }

    public void QuitGame() //Cierra el juego, gracias por jugar o pispear
    {
        Application.Quit();
    }

    public void loadLevel(string level)
    {
        if (saveGameManager.saveData.stamine > 0)
        {
            SceneManager.LoadScene(level);
            saveGameManager.saveData.stamine -= 1;
        }
        else
        {
            StartCoroutine(ShakeImagen()); // shake de la imagen
        }
    }

    IEnumerator ShakeImagen()
    {
        Vector3 posicionInicial = imageStamina.transform.position;
        float tiempoInicio = Time.time;

        while (Time.time - tiempoInicio < shakeDuration)
        {
            float offsetX = Random.Range(-intensityShake, intensityShake);
            float offsetY = Random.Range(-intensityShake, intensityShake);

            imageStamina.transform.position = new Vector3(posicionInicial.x + offsetX, posicionInicial.y + offsetY, posicionInicial.z);

            yield return null;
        }
        imageStamina.transform.position = posicionInicial;
    }

    /*public void PlaySfx()
    {
        AudioManager.Instance.PlaySFX(0);
    }*/
}
