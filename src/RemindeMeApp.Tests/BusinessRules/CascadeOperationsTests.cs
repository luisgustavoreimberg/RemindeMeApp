using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RemindeMeApp.Backend.Services;
using RemindeMeApp.Shared.Models;
using RemindeMeApp.Tests.Infrastructure;
using Xunit;

namespace RemindeMeApp.Tests.BusinessRules;

public class CascadeOperationsTests : TestBase
{
    [Fact]
    public async Task CompleteTask_WithActiveSubtasks_ShouldCompleteSubtasks()
    {
        // Arrange
        var taskService = new TaskService(DbContext);
        
        var task = new TaskItem { Titulo = "Main Task", IsAtivo = true };
        DbContext.TaskItems.Add(task);
        await DbContext.SaveChangesAsync();
        
        var subtask1 = new Subtask { Titulo = "Sub 1", ParentTaskId = task.Id, IsAtivo = true };
        var subtask2 = new Subtask { Titulo = "Sub 2", ParentTaskId = task.Id, IsAtivo = true };
        DbContext.Subtasks.AddRange(subtask1, subtask2);
        await DbContext.SaveChangesAsync();

        // Act
        await taskService.ToggleActiveAsync(task.Id);

        // Assert
        var subtasks = await DbContext.Set<Subtask>().Where(s => s.ParentTaskId == task.Id).ToListAsync();
        subtasks.Should().OnlyContain(s => s.IsAtivo == false);
    }
}
