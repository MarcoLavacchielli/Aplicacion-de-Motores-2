using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public Slider barraProgreso;
    public string nombreEscena;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        StartCoroutine(CargarJuego(nombreEscena));
    }

    private IEnumerator CargarJuego(string nombreEscena)
    {
        AsyncOperation cargaOperacion = SceneManager.LoadSceneAsync(nombreEscena);

        while (!cargaOperacion.isDone)
        {
            float progreso = Mathf.Clamp01(cargaOperacion.progress / 0.9f);
            barraProgreso.value = progreso;
            yield return null;
        }
        SceneManager.LoadScene(nombreEscena);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("Checkeo");
        // Verifica si la escena cargada es la que especificaste en sceneToGo
        if (scene.name == nombreEscena)
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