using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RemindeMeApp.Backend.Data;
using RemindeMeApp.Shared.Models;
using RemindeMeApp.Shared.Services;

namespace RemindeMeApp.Backend.Services;

public class TaskService : ITaskService
{
    private readonly ApplicationDbContext _dbContext;

    public TaskService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        return await _dbContext.TaskItems
            .Include(t => t.Tag)
            .Include(t => t.Subtasks)
            .ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await _dbContext.TaskItems
            .Include(t => t.Tag)
            .Include(t => t.Subtasks)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TaskItem> CreateAsync(TaskItem task)
    {
        if (string.IsNullOrWhiteSpace(task.Titulo))
            throw new System.ArgumentException("Título é obrigatório", nameof(task.Titulo));

        if (task.Tag != null && task.Tag.Id == 0)
        {
            var existingTag = await _dbContext.Tags.FirstOrDefaultAsync(t => t.Nome == task.Tag.Nome);
            if (existingTag != null)
            {
                task.Tag = null;
                task.TagId = existingTag.Id;
            }
            else
            {
                if (string.IsNullOrEmpty(task.Tag.CorHexadecimal))
                {
                    task.Tag.CorHexadecimal = "#808080";
                }
            }
        }

        _dbContext.TaskItems.Add(task);
        await _dbContext.SaveChangesAsync();
        return task;
    }

    // For test compatibility if needed
    public async Task<TaskItem> CreateTaskAsync(TaskItem task, string? tagName = null)
    {
        if (!string.IsNullOrEmpty(tagName))
        {
            task.Tag = new Tag { Nome = tagName };
        }
        return await CreateAsync(task);
    }

    public async Task<TaskItem> UpdateAsync(TaskItem task)
    {
        if (task.Tag != null && task.Tag.Id == 0)
        {
            var existingTag = await _dbContext.Tags.FirstOrDefaultAsync(t => t.Nome == task.Tag.Nome);
            if (existingTag != null)
            {
                task.Tag = null;
                task.TagId = existingTag.Id;
            }
            else
            {
                if (string.IsNullOrEmpty(task.Tag.CorHexadecimal))
                {
                    task.Tag.CorHexadecimal = "#808080";
                }
            }
        }

        _dbContext.TaskItems.Update(task);
        await _dbContext.SaveChangesAsync();
        return task;
    }

    public async Task DeleteAsync(int id)
    {
        var task = await _dbContext.TaskItems
            .Include(t => t.Subtasks)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task != null)
        {
            _dbContext.TaskItems.Remove(task);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task ToggleActiveAsync(int id)
    {
        var task = await _dbContext.TaskItems
            .Include(t => t.Subtasks)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task != null)
        {
            task.IsAtivo = !task.IsAtivo;

            if (!task.IsAtivo)
            {
                foreach (var subtask in task.Subtasks)
                {
                    subtask.IsAtivo = false;
                }
            }

            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task CompleteTaskAsync(int id)
    {
        var task = await _dbContext.TaskItems
            .Include(t => t.Subtasks)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task != null && task.IsAtivo)
        {
            task.IsAtivo = false;
            foreach (var subtask in task.Subtasks)
            {
                subtask.IsAtivo = false;
            }
            await _dbContext.SaveChangesAsync();
        }
    }

    // For test compatibility if needed
    public async Task<Subtask> CreateSubtaskAsync(Subtask subtask)
    {
        if (subtask.ParentTaskId <= 0)
            throw new System.ArgumentException("ParentTaskId é obrigatório", nameof(subtask.ParentTaskId));

        _dbContext.Subtasks.Add(subtask);
        await _dbContext.SaveChangesAsync();
        return subtask;
    }
}
