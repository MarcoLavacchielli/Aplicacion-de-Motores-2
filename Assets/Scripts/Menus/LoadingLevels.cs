using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingLevels : MonoBehaviour
{
    public Slider barraProgreso;
    public string nombreEscena;

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
}
