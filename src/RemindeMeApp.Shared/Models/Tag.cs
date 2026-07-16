using System.Collections.Generic;

namespace RemindeMeApp.Shared.Models;

/// <summary>
/// Represents a tag that can be assigned to tasks.
/// </summary>
public class Tag
{
    /// <summary>
    /// The unique identifier of the tag.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the tag.
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// The hexadecimal color of the tag.
    /// </summary>
    public string CorHexadecimal { get; set; } = string.Empty;

    /// <summary>
    /// The tasks associated with this tag.
    /// </summary>
    public ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
}
