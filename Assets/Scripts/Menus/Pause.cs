using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseCanvas;
    [SerializeField] private Player playerInput;
    [SerializeField] private bool isPaused = false;

    public GameObject confirmationCanvas;

    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject joystick;

    private void Awake()
    {
        playerInput = new Player();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Update()
    {
        if (playerInput.GameMain.Pause.triggered)
        {
            Debug.Log("Flag");
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pausar el tiempo en el juego
            pauseCanvas.SetActive(true); // Mostrar el canvas de pausa
        }
        else
        {
            Time.timeScale = 1f; // Reanudar el tiempo en el juego
            pauseCanvas.SetActive(false); // Ocultar el canvas de pausa
        }
    }

    public void securitycanvas()
    {
        pauseCanvas.SetActive(false);
        confirmationCanvas.SetActive(true);
    }

    public void securityNO()
    {
        pauseCanvas.SetActive(true);
        confirmationCanvas.SetActive(false);
    }

    public void loadscene(string scenename) //carga una escena especifica por nombre
    {
        Time.timeScale = 1f; // Reanudar el tiempo en el juego
        SceneManager.LoadScene(scenename);
    }

    public void changeArrow()
    {
        arrow.SetActive(true);
        joystick.SetActive(false);
    }

    public void changeStick()
    {
        arrow.SetActive(false);
        joystick.SetActive(true);
    }

}
