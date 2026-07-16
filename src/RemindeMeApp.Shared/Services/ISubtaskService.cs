using System.Collections.Generic;
using System.Threading.Tasks;
using RemindeMeApp.Shared.Models;

namespace RemindeMeApp.Shared.Services;

/// <summary>
/// Service interface for subtask management.
/// </summary>
public interface ISubtaskService
{
    /// <summary>
    /// Retrieves all subtasks for a given parent task.
    /// </summary>
    Task<IEnumerable<Subtask>> GetByParentTaskIdAsync(int parentTaskId);

    /// <summary>
    /// Retrieves a subtask by its ID.
    /// </summary>
    Task<Subtask?> GetByIdAsync(int id);

    /// <summary>
    /// Creates a new subtask.
    /// </summary>
    Task<Subtask> CreateAsync(Subtask subtask);

    /// <summary>
    /// Updates an existing subtask.
    /// </summary>
    Task<Subtask> UpdateAsync(Subtask subtask);

    /// <summary>
    /// Deletes a subtask by its ID.
    /// </summary>
    Task DeleteAsync(int id);

    /// <summary>
    /// Toggles the active status of a subtask.
    /// </summary>
    Task ToggleActiveAsync(int id);
}
