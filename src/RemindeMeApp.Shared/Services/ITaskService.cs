using System.Collections.Generic;
using System.Threading.Tasks;
using RemindeMeApp.Shared.Models;

namespace RemindeMeApp.Shared.Services;

/// <summary>
/// Service interface for task management.
/// </summary>
public interface ITaskService
{
    /// <summary>
    /// Retrieves all tasks.
    /// </summary>
    Task<IEnumerable<TaskItem>> GetAllAsync();

    /// <summary>
    /// Retrieves a task by its ID.
    /// </summary>
    Task<TaskItem?> GetByIdAsync(int id);

    /// <summary>
    /// Creates a new task.
    /// </summary>
    Task<TaskItem> CreateAsync(TaskItem task);

    /// <summary>
    /// Updates an existing task.
    /// </summary>
    Task<TaskItem> UpdateAsync(TaskItem task);

    /// <summary>
    /// Deletes a task by its ID.
    /// </summary>
    Task DeleteAsync(int id);

    /// <summary>
    /// Toggles the active status of a task.
    /// </summary>
    Task ToggleActiveAsync(int id);
}
