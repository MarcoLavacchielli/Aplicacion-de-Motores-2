using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class JsonSaveGameManager : MonoBehaviour
{

    [SerializeField] JsonSaveGameKey saveData = new JsonSaveGameKey();
    string path;

    void Awake()
    {
        //path = Application.dataPath + "/SaveData.save";
        path = Application.persistentDataPath + "/SaveData.save";

        Debug.Log(path);

        LoadGame();
    }

    void Update() //Esto es para testing
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DeleteGame();
        }
    }

    private void SaveGame()
    {
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(path, json);

        Debug.Log(json);
    }

    private void LoadGame()
    {
        string json = File.ReadAllText(path);
        JsonUtility.FromJsonOverwrite(json, saveData);

        Debug.Log(json);
    }

    private void DeleteGame()
    {
        File.Delete(path);

        Debug.Log("Archivo borrado");

        saveData = new JsonSaveGameKey(); //valores predeterminados
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