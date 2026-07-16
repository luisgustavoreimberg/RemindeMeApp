using System.Threading.Tasks;
using RemindeMeApp.Shared.Models;

namespace RemindeMeApp.Shared.Services;

/// <summary>
/// Service interface for Pomodoro technique management.
/// </summary>
public interface IPomodoroService
{
    /// <summary>
    /// Starts a Pomodoro focus session.
    /// </summary>
    Task<TimerSession> StartFocusAsync(int referenceId, bool isSubtask, int durationSeconds);

    /// <summary>
    /// Starts a Pomodoro break session.
    /// </summary>
    Task<TimerSession> StartBreakAsync(int durationSeconds);

    /// <summary>
    /// Stops the active Pomodoro session.
    /// </summary>
    Task StopAsync(int sessionId);

    /// <summary>
    /// Retrieves the currently active Pomodoro session.
    /// </summary>
    Task<TimerSession?> GetActiveSessionAsync();
}
