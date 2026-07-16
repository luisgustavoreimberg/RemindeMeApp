using System;
using System.Collections.Generic;

namespace RemindeMeApp.Shared.Models;

/// <summary>
/// Represents a main task or reminder.
/// </summary>
public class TaskItem
{
    /// <summary>
    /// The unique identifier of the task.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The title of the task.
    /// </summary>
    public string Titulo { get; set; } = string.Empty;

    /// <summary>
    /// The optional description of the task.
    /// </summary>
    public string? Descricao { get; set; }

    /// <summary>
    /// The optional date and time for the reminder.
    /// </summary>
    public DateTime? DataHoraLembrete { get; set; }

    /// <summary>
    /// Indicates whether the task is active. Default is true.
    /// </summary>
    public bool IsAtivo { get; set; } = true;

    /// <summary>
    /// The date and time when the task was completed.
    /// </summary>
    public DateTime? DataHoraFinalizacao { get; set; }

    /// <summary>
    /// The optional associated tag identifier.
    /// </summary>
    public int? TagId { get; set; }

    /// <summary>
    /// The optional associated tag.
    /// </summary>
    public Tag? Tag { get; set; }

    /// <summary>
    /// Total time spent on this task in seconds.
    /// </summary>
    public int TempoTotalGastoSegundos { get; set; } = 0;

    /// <summary>
    /// The subtasks associated with this task.
    /// </summary>
    public ICollection<Subtask> Subtasks { get; set; } = new List<Subtask>();
}
