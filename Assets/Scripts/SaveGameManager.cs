using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SaveGameManager : MonoBehaviour
{

    [SerializeField] int currency = 0;
    [SerializeField] string playerName = "null";
    [SerializeField] int tutorial = 1;
    [SerializeField] int level1 = 0;
    [SerializeField] int level2 = 0;
    //[SerializeField] TextMeshProUGUI[] textShowingStats;

    void Awake()
    {
        LoadGame();
        if (PlayerPrefs.HasKey(SaveGameKey.levelTutorialKey)) tutorial = PlayerPrefs.GetInt(SaveGameKey.levelTutorialKey, SaveGameKey.levelUnlock);
        else tutorial = SaveGameKey.levelUnlock;
    }

    void Update() //Esto es para testing
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("moneda: "+currency+", nombre: "+playerName+", tuto: "+tutorial+", nivel1: "+level1+", nivel2: "+level2);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DeleteGame();
        }
    }

    private void SaveGame()
    {
        PlayerPrefs.SetInt(SaveGameKey.currencyKey, currency);
        PlayerPrefs.SetString(SaveGameKey.playerNameKey, playerName);
        PlayerPrefs.SetInt(SaveGameKey.levelTutorialKey, tutorial);
        PlayerPrefs.SetInt(SaveGameKey.level1Key, level1);
        PlayerPrefs.SetInt(SaveGameKey.level2Key, level2);

        //PlayerPrefs.Save();

        Debug.Log("Save game");
    }

    private void LoadGame()
    {
        //Si el player tiene la key de currency, se carga su valor, si no, se le ponen sus valores default

        if (PlayerPrefs.HasKey(SaveGameKey.currencyKey)) currency = PlayerPrefs.GetInt(SaveGameKey.currencyKey, SaveGameKey.currencyDefault);
        else currency = SaveGameKey.currencyDefault;

        //Lo mismo pero con el nombre

        if (PlayerPrefs.HasKey(SaveGameKey.playerNameKey)) playerName = PlayerPrefs.GetString(SaveGameKey.playerNameKey, "null");
        else playerName = "null";

        //Para los niveles

        if (PlayerPrefs.HasKey(SaveGameKey.levelTutorialKey)) tutorial = PlayerPrefs.GetInt(SaveGameKey.levelTutorialKey, SaveGameKey.levelUnlock);
        else tutorial = SaveGameKey.levelUnlock;

        // 1 y 2

        if (PlayerPrefs.HasKey(SaveGameKey.level1Key)) tutorial = PlayerPrefs.GetInt(SaveGameKey.level1Key, SaveGameKey.levelBlock);
        else tutorial = SaveGameKey.levelBlock;

        if (PlayerPrefs.HasKey(SaveGameKey.level2Key)) tutorial = PlayerPrefs.GetInt(SaveGameKey.level2Key, SaveGameKey.levelBlock);
        else tutorial = SaveGameKey.levelBlock;

        Debug.Log("Loading game");
    }

    public void DeleteGame()
    {

        PlayerPrefs.DeleteAll();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Debug.Log("Data Deleted");
    }

    private void OnApplicationQuit() //Cerrar la aplicacion
    {
        SaveGame();
    }

    private void OnApplicationPause(bool pause) //En Mobile pasa cuando minimizamos la app
    {
        if (pause) SaveGame();
    }

    /*private void OnApplicationFocus(bool focus) //En Mobile pasa cuando entramos a la app
    {
        if (!focus) SaveGame();
    }*/

    private void OnDestroy()
    {
        SaveGame();
    }

}
