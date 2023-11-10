using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Notifications.Android;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance { get; private set; }

    AndroidNotificationChannel notifChannel;

    private void Awake()
    {
        if (Instance != this && Instance != null) Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        //opcionar de ser un booleando
        //AndroidNotificationCenter.CancelAllDisplayedNotifications();
        //AndroidNotificationCenter.CancelAllScheduledNotifications();
        //
        notifChannel = new AndroidNotificationChannel()
        {
            Id = "Reminder_notif_ch",
            Name = "Reminder Notification",
            Description = "Reminder to login",
            Importance = Importance.High
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notifChannel);

        DisplayNotification("Vuelve", "te necesito", IconSelecter.icon_reminder, IconSelecter.icon_reminderbig, DateTime.Now.AddHours(36));
    }

    public int DisplayNotification(string title, string text, IconSelecter iconSmall, IconSelecter iconBig, DateTime firetime)
    {
        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.SmallIcon = iconSmall.ToString();
        notification.LargeIcon = iconBig.ToString();
        notification.FireTime = firetime;

        return AndroidNotificationCenter.SendNotification(notification, notifChannel.Id);
    }

    public void CancelNotification(int id)
    {
        AndroidNotificationCenter.CancelScheduledNotification(id);
    }

    public enum IconSelecter
    {
        icon_reminder,
        icon_reminderbig
    }

}
