using System.Threading.Tasks;
using RemindeMeApp.Shared.Models;

namespace RemindeMeApp.Shared.Services;

/// <summary>
/// Service interface for free time tracking.
/// </summary>
public interface ITimeTrackerService
{
    /// <summary>
    /// Starts tracking time for a given reference (Task or Subtask).
    /// </summary>
    Task<TimerSession> StartTrackingAsync(int referenceId, bool isSubtask);

    /// <summary>
    /// Stops the active tracking session and records the time.
    /// </summary>
    Task StopTrackingAsync(int sessionId);

    /// <summary>
    /// Retrieves the currently active timer session.
    /// </summary>
    Task<TimerSession?> GetActiveSessionAsync();

    /// <summary>
    /// Adds manual time to a task or subtask.
    /// </summary>
    Task AddManualTimeAsync(int referenceId, bool isSubtask, int secondsToAdd);
}
