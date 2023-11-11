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
    int _currentStamina;

    DateTime _nextStaminaTime, _lastStaminaTime;

    bool recharging;

    [SerializeField] TextMeshProUGUI _staminaText = null, _timerText = null;

    [SerializeField] string _notifTitle = "Full Stamina";
    [SerializeField] string _notifText = "Tenes la estamina llena";
    [SerializeField] IconSelecter _smallIcon = IconSelecter.icon_reminder;
    [SerializeField] IconSelecter _bigIcon = IconSelecter.icon_reminderbig;
    TimeSpan timer;
    int id;

    void OnEnable()
    {
        LoadGame();
        StartCoroutine(RechargeStamina());

        if(_currentStamina < _maxStamina)
        {
            timer = _nextStaminaTime - DateTime.Now;
            id = NotificationManager.Instance.DisplayNotification(_notifTitle, _notifText, _smallIcon, _bigIcon, AddDuration(DateTime.Now, ((_maxStamina - _currentStamina + 1) * _timerToRecharge) + 1 + (float)timer.TotalSeconds));
        }
    }

    //

    public bool HasEnoughStamina(int stamina) => _currentStamina * stamina >= 0;

    public void UseStamina(int staminaToUse)
    {
        if(_currentStamina - staminaToUse >= 0)
        {
            _currentStamina -= staminaToUse;
            UpdateStaminaText();

            NotificationManager.Instance.CancelNotification(id);

            id = NotificationManager.Instance.DisplayNotification(_notifTitle, _notifText, _smallIcon, _bigIcon, AddDuration(DateTime.Now, ((_maxStamina - _currentStamina + 1) * _timerToRecharge) + 1 + (float)timer.TotalSeconds));


            if (!recharging)
            {
                _nextStaminaTime = AddDuration(DateTime.Now, _timerToRecharge);
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
            SaveGameKey();

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
        PlayerPrefs.SetInt(PlayerPrefsKeys.currentStaminaKey, _currentStamina);
        PlayerPrefs.SetString(PlayerPrefsKeys.currentStaminaKey, _nextStaminaTime.ToString());
        PlayerPrefs.SetString(PlayerPrefsKeys.currentStaminaKey, _lastStaminaTime.ToString());
    }

    void LoadGame()
    {
        _currentStamina = PlayerPrefs.GetInt(PlayerPrefskeys.currentStaminaKeym _maxStamina);
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
