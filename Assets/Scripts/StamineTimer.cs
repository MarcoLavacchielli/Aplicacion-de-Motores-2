using UnityEngine;
using TMPro;
using System;
using static NotificationManager;

public class StamineTimer : MonoBehaviour
{
    public JsonSaveGameManager saveGameManager;
    public TextMeshProUGUI countdownText;

    private const string PlayerPrefsKey = "LastStaminaRechargeTime";
    [SerializeField] private float rechargeTimeInSeconds = 300f; // 5 minutos en segundos
    private float timeToNextStaminaRecharge;


    [SerializeField] string _notifTitle = "Estamina llena";
    [SerializeField] string _notifText = "La estamina se recargo en su totalidad";
    [SerializeField] IconSelecter _smallIcon = IconSelecter.icon_reminder;
    [SerializeField] IconSelecter _bigIcon = IconSelecter.icon_reminderbig;
    TimeSpan timer;
    int id;

    void Awake()
    {
        saveGameManager = FindObjectOfType<JsonSaveGameManager>();

        if (saveGameManager == null)
        {
            Debug.LogError("JsonSaveGameManager no encontrado en la escena.");
        }
    }

    void Start()
    {
        timeToNextStaminaRecharge = rechargeTimeInSeconds;

        // Recuperar la última hora de recarga almacenada
        if (PlayerPrefs.HasKey(PlayerPrefsKey))
        {
            string savedTimeString = PlayerPrefs.GetString(PlayerPrefsKey);
            DateTime lastRechargeTime = DateTime.Parse(savedTimeString);
            TimeSpan elapsedTime = DateTime.Now - lastRechargeTime;

            // Restar el tiempo transcurrido al tiempo restante
            timeToNextStaminaRecharge -= (float)elapsedTime.TotalSeconds;

            // Asegurarse de que el tiempo restante no sea negativo
            timeToNextStaminaRecharge = Mathf.Max(0f, timeToNextStaminaRecharge);
        }
    }

    void OnApplicationQuit()
    {
        // Almacenar la última hora de recarga al salir de la aplicación
        PlayerPrefs.SetString(PlayerPrefsKey, DateTime.Now.ToString());
        PlayerPrefs.Save();
    }

    void Update()
    {
        // Actualizar el temporizador si la estamina no es 10
        if (saveGameManager.saveData.stamine < 10)
        {
            timeToNextStaminaRecharge -= Time.deltaTime;
        }
        else
        {
            // Si la estamina es 10, reiniciar el temporizador y el prefab
            timeToNextStaminaRecharge = rechargeTimeInSeconds;
        }

        // Mostrar la cuenta regresiva en el texto
        int minutes = Mathf.FloorToInt(timeToNextStaminaRecharge / 60);
        int seconds = Mathf.FloorToInt(timeToNextStaminaRecharge % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Recargar estamina si ha pasado el tiempo y la estamina no es 10
        if (timeToNextStaminaRecharge <= 0 && saveGameManager.saveData.stamine < 10)
        {
            saveGameManager.saveData.stamine++;

            // Reiniciar el temporizador
            timeToNextStaminaRecharge = rechargeTimeInSeconds;

            // Almacenar la hora de recarga actual
            PlayerPrefs.SetString(PlayerPrefsKey, DateTime.Now.ToString());
            PlayerPrefs.Save();

            // Agregar la notificación cuando la estamina se recargue
            SendStaminaRechargedNotification();
        }

        // Asegurarse de que la estamina no supere el límite
        saveGameManager.saveData.stamine = Mathf.Clamp(saveGameManager.saveData.stamine, 0, 10);
    }

    public void fillStamina()
    {
        saveGameManager.saveData.stamine += 10;

        // Almacenar la hora de recarga actual
        PlayerPrefs.SetString(PlayerPrefsKey, DateTime.Now.ToString());
        PlayerPrefs.Save();

        // Agregar la notificación cuando la estamina se recargue
        SendStaminaRechargedNotification();
    }

    // Función para enviar la notificación cuando la estamina se recargue
    private void SendStaminaRechargedNotification()
    {
        if (NotificationManager.Instance != null)
        {
            // Cancelar la notificación anterior antes de mostrar una nueva
            NotificationManager.Instance.CancelNotification(id);
            //Debug.Log("NOTI Cancelada");

            // Mostrar la nueva notificación
            id = NotificationManager.Instance.DisplayNotification(_notifTitle, _notifText, _smallIcon, _bigIcon, DateTime.Now);
            //Debug.Log("NOTI Creada");
        }
        else
        {
            Debug.LogError("NotificationManager.Instance is null");
        }
    }
}