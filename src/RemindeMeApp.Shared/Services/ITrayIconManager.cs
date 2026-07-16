using System;

namespace RemindeMeApp.Shared.Services;

public interface ITrayIconManager
{
    void Initialize();
    void ShowNotification(string title, string message); // Baloon tip
}
