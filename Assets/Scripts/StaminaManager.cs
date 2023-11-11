using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using static NotificationManager;

public class StaminaManager : MonoBehaviour
{

    [SerializeField] int _maxStamina = 10;
    [SerializeField] float _timerToRecharge = 15f;
    public int _currentStamina;

    DateTime _nextStaminaTime, _lastStaminaTime;

    bool recharging;

    [SerializeField] TextMeshProUGUI _staminaText = null, _timerText = null;

    [SerializeField] string _notifTitle = "Full Stamina";
    [SerializeField] string _notifText = "Tenes la estamina llena";
    [SerializeField] IconSelecter _smallIcon = IconSelecter.icon_reminder;
    [SerializeField] IconSelecter _bigIcon = IconSelecter.icon_reminderbig;
    TimeSpan timer;
    int id;

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.O))
        {
            UseStamina(1);
        }*/
    }

    void OnEnable()
    {
        LoadGame();
        StartCoroutine(RechargeStamina());

        if (_currentStamina < _maxStamina)
        {
            timer = _nextStaminaTime - DateTime.Now;
            id = NotificationManager.Instance.DisplayNotification(_notifTitle, _notifText, _smallIcon, _bigIcon, AddDuration(DateTime.Now, ((_maxStamina - _currentStamina + 1) * _timerToRecharge) + 1 + (float)timer.TotalSeconds));
        }
    }

    //

    public bool HasEnoughStamina(int stamina) => _currentStamina * stamina >= 0;

    public void UseStamina(int staminaToUse)
    {
        if (_currentStamina - staminaToUse >= 0)
        {
            _currentStamina -= staminaToUse;
            UpdateStaminaText();

            // Cancelar la notificación anterior
            NotificationManager.Instance.CancelNotification(id);

            // Reiniciar el temporizador al valor completo si la currentStamina es 10
            if (_currentStamina >= 9)
            {

                _nextStaminaTime = AddDuration(DateTime.Now, _timerToRecharge);

            }

            // Mostrar la nueva notificación
            if (_currentStamina < _maxStamina)
            {
                id = NotificationManager.Instance.DisplayNotification(_notifTitle, _notifText, _smallIcon, _bigIcon, AddDuration(DateTime.Now, ((_maxStamina - _currentStamina + 1) * _timerToRecharge) + 1 + (float)timer.TotalSeconds));
            }

            // Iniciar la recarga solo si no se está recargando actualmente
            if (!recharging)
            {
                StartCoroutine(RechargeStamina());
            }
            Debug.Log("Go to level....");
        }
        else
        {
            Debug.Log("Sin stamina");
        }
    }

    IEnumerator RechargeStamina()
    {
        UpdateTimerText();
        UpdateStaminaText();
        recharging = true;

        //
        while(_currentStamina < _maxStamina)
        {
            DateTime currentTime = DateTime.Now;
            DateTime nextTime = _nextStaminaTime;

            //
            bool staminaAdd = false;

            while(currentTime > nextTime)
            {
                if (_currentStamina >= _maxStamina) break;

                _currentStamina += 1;
                staminaAdd = true;

                //

                DateTime timeToAdd = nextTime;

                //

                if (_lastStaminaTime > nextTime) timeToAdd = _lastStaminaTime;

                //

                nextTime = AddDuration(timeToAdd, _timerToRecharge);
            }

            if (staminaAdd)
            {
                _nextStaminaTime = nextTime;
                _lastStaminaTime = DateTime.Now;
            }

            UpdateTimerText();
            UpdateStaminaText();
            SaveGame();  // duda esto
             
            yield return new WaitForEndOfFrame();
        }

        NotificationManager.Instance.CancelNotification(id);
        recharging = true;
    }

    private DateTime AddDuration(DateTime date, float duration)
    {
        return date.AddSeconds(duration);
        // return date.AddMinutes(duration);
    }

    private void UpdateStaminaText()
    {
        _staminaText.text = $"{_currentStamina} / {_maxStamina}";
    }

    private void UpdateTimerText()
    {
        if (_currentStamina >= _maxStamina)
        {
            _timerText.text = "Full Stamina";
            return;
        }

        TimeSpan timer = _nextStaminaTime - DateTime.Now;
        _timerText.text = timer.Minutes.ToString("00") + ":" + timer.Seconds.ToString("00");
    }

    void SaveGame()
    {
        PlayerPrefs.SetInt(PlayerPrefskeys.currentStaminaKey.ToString(), _currentStamina);
        PlayerPrefs.SetString(PlayerPrefskeys.nextStaminaKey, _nextStaminaTime.ToString());
        PlayerPrefs.SetString(PlayerPrefskeys.lastStaminaKey, _lastStaminaTime.ToString());
    }

    void LoadGame()
    {
        _currentStamina = PlayerPrefs.GetInt(PlayerPrefskeys.currentStaminaKey.ToString(), _maxStamina);
        _nextStaminaTime = StringToDateTime(PlayerPrefs.GetString(PlayerPrefskeys.nextStaminaKey));
        _lastStaminaTime = StringToDateTime(PlayerPrefs.GetString(PlayerPrefskeys.lastStaminaKey));
    }

    DateTime StringToDateTime(string date)
    {
        if (string.IsNullOrEmpty(date))
            return DateTime.Now;
        else
            return DateTime.Parse(date);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause) SaveGame();
    }

    private void OnDisable()
    {
        SaveGame();
    }

}
