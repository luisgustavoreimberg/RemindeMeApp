using System.Collections.Generic;
using System.Threading.Tasks;
using RemindeMeApp.Shared.Models;

namespace RemindeMeApp.Shared.Services;

/// <summary>
/// Service interface for tag management.
/// </summary>
public interface ITagService
{
    /// <summary>
    /// Retrieves all tags.
    /// </summary>
    Task<IEnumerable<Tag>> GetAllAsync();

    /// <summary>
    /// Retrieves a tag by its ID.
    /// </summary>
    Task<Tag?> GetByIdAsync(int id);

    /// <summary>
    /// Creates a new tag.
    /// </summary>
    Task<Tag> CreateAsync(Tag tag);

    /// <summary>
    /// Updates an existing tag.
    /// </summary>
    Task<Tag> UpdateAsync(Tag tag);

    /// <summary>
    /// Deletes a tag by its ID.
    /// </summary>
    Task DeleteAsync(int id);

    /// <summary>
    /// Retrieves an existing tag by name or creates a new one if it does not exist.
    /// </summary>
    Task<Tag> GetOrCreateByNameAsync(string name);
}
