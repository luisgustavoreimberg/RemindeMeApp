namespace RemindeMeApp.Shared.Models;

/// <summary>
/// Defines the types of timer tracking available.
/// </summary>
public enum TimerType
{
    /// <summary>
    /// Free tracking timer, not bound by specific duration.
    /// </summary>
    FreeTimer,

    /// <summary>
    /// Pomodoro technique timer, bound by expected duration.
    /// </summary>
    Pomodoro
}
