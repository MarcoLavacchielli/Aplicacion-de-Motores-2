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

    public void SwitchPanel(int index)  //Setea por indice, el panel a activar
    {
        if (index >= 0 && index < panels.Length)
        {
            panels[index].SetActive(!panels[index].activeSelf);
        }
    }

    public void TogglePanel(GameObject panelToToggle) //setea por editor, el panel a desactivar
    {
        panelToToggle.SetActive(!panelToToggle.activeSelf);
    }

    public void loadscene(string scenename) //carga una escena especifica por nombre
    {
        SceneManager.LoadScene(scenename);
    }

    public void QuitGame() //Cierra el juego, gracias por jugar o pispear
    {
        Application.Quit();
    }
}
