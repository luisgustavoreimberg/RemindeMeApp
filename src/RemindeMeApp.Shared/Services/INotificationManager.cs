namespace RemindeMeApp.Shared.Services;

public interface INotificationManager
{
    void ShowNotification(string title, string message);
    bool IsDoNotDisturbEnabled();
}
