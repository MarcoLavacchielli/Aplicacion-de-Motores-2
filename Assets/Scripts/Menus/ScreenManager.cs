using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private StaminaManager staminaManager;
    [SerializeField] private GameObject imageStamina;
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private float intensityShake = 0.1f;

    private int currentPanelIndex = -1;

    void Awake()
    {
        staminaManager = FindObjectOfType<StaminaManager>();
        //ShowPanel(currentPanelIndex); //prueba
    }

    public void SwitchPanel(int index)
    {
        if (index >= 0 && index < panels.Length && index != currentPanelIndex)
        {
            HideCurrentPanel();
            currentPanelIndex = index;
            ShowPanel(index);
            AudioManager.Instance.PlaySFX(0); 
        }
    }

    private void ShowPanel(int index)
    {
        if (index >= 0 && index < panels.Length)
        {
            panels[index].SetActive(true);
        }
    }

    private void HideCurrentPanel()
    {
        if (currentPanelIndex >= 0 && currentPanelIndex < panels.Length)
        {
            panels[currentPanelIndex].SetActive(false);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevel(string level)
    {
        if (staminaManager.HasEnoughStamina(1))
        {
            staminaManager.UseStamina(1);
            SceneManager.LoadScene(level);
        }
        else
        {
            StartCoroutine(ShakeImage());
        }
    }

    // Error panels //
    public void panelNotClose(int index)
    {
        panels[index].SetActive(false);
    }

    public void panelNotOpen(int index)
    {
        panels[index].SetActive(true);
    }
    // Error panels //

    IEnumerator ShakeImage()
    {
        Vector3 initialPosition = imageStamina.transform.position;
        float startTime = Time.time;

        while (Time.time - startTime < shakeDuration)
        {
            float offsetX = Random.Range(-intensityShake, intensityShake);
            float offsetY = Random.Range(-intensityShake, intensityShake);

            imageStamina.transform.position = new Vector3(initialPosition.x + offsetX, initialPosition.y + offsetY, initialPosition.z);

            yield return null;
        }

        imageStamina.transform.position = initialPosition;
    }
}