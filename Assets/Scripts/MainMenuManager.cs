using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    /*[SerializeField] private GameObject initialScreenPanel;
    [SerializeField] private GameObject titleScreenPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject levelSelectorPanel;
    [SerializeField] private GameObject storePanel;*/

    [SerializeField] private GameObject[] panels;

    public void SwitchPanel(int index)
    {
        if (index >= 0 && index < panels.Length)
        {
            panels[index].SetActive(!panels[index].activeSelf);
        }
    }

    public void TogglePanel(GameObject panelToToggle)
    {
        panelToToggle.SetActive(!panelToToggle.activeSelf);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
