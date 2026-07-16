using System;

namespace RemindeMeApp.Shared.Models;

/// <summary>
/// Represents a time tracking session (FreeTimer or Pomodoro).
/// </summary>
public class TimerSession
{
    /// <summary>
    /// The unique identifier of the timer session.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The reference identifier (can be a TaskId or SubtaskId).
    /// </summary>
    public int ReferenceId { get; set; }

    /// <summary>
    /// Indicates whether the ReferenceId points to a Subtask.
    /// </summary>
    public bool IsSubtask { get; set; }

    /// <summary>
    /// The start time of the session.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// The type of the timer session.
    /// </summary>
    public TimerType Tipo { get; set; }

    /// <summary>
    /// Expected duration in seconds (applicable for Pomodoro).
    /// </summary>
    public int? DuracaoEsperadaSegundos { get; set; }

    /// <summary>
    /// Indicates whether the session is currently paused.
    /// </summary>
    public bool IsPaused { get; set; } = false;

    /// <summary>
    /// The elapsed time in seconds when the session was paused.
    /// </summary>
    public int PausedElapsedSeconds { get; set; } = 0;
}
