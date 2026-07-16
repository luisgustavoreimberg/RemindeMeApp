namespace RemindeMeApp.Shared.Models;

/// <summary>
/// Represents a subtask belonging to a main task.
/// </summary>
public class Subtask
{
    /// <summary>
    /// The unique identifier of the subtask.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The identifier of the parent task.
    /// </summary>
    public int ParentTaskId { get; set; }

    /// <summary>
    /// The parent task.
    /// </summary>
    public TaskItem ParentTask { get; set; } = null!;

    /// <summary>
    /// The title of the subtask.
    /// </summary>
    public string Titulo { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the subtask is active. Default is true.
    /// </summary>
    public bool IsAtivo { get; set; } = true;

    /// <summary>
    /// Total time spent on this subtask in seconds.
    /// </summary>
    public int TempoTotalGastoSegundos { get; set; } = 0;
}
