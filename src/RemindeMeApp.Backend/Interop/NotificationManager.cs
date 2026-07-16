using System;
using CommunityToolkit.WinUI.Notifications;
using Microsoft.Win32;
using RemindeMeApp.Shared.Services;

namespace RemindeMeApp.Backend.Interop;

public class NotificationManager : INotificationManager
{
    public void ShowNotification(string title, string message)
    {
        if (IsDoNotDisturbEnabled())
        {
            // Optional: log that notification was suppressed or sent silently
            // We can still send it and Windows will route it to the Action Center silently.
        }

        new ToastContentBuilder()
            .AddText(title)
            .AddText(message)
            .Show();
    }

    public bool IsDoNotDisturbEnabled()
    {
        try
        {
            using var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\QuietHours");
            if (key != null)
            {
                var value = key.GetValue("UserSetting");
                if (value is int quietHoursSetting)
                {
                    // 0 = Off
                    // 1 = Priority Only
                    // 2 = Alarms Only
                    return quietHoursSetting != 0;
                }
            }
        }
        catch
        {
            // Ignore registry read errors and assume it's off
        }

        return false;
    }
}
