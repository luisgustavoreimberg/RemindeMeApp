using System;
using System.Threading.Tasks;
using FluentAssertions;
using RemindeMeApp.Backend.Services;
using RemindeMeApp.Shared.Models;
using RemindeMeApp.Tests.Infrastructure;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace RemindeMeApp.Tests.BusinessRules;

public class TagCreationTests : TestBase
{
    [Fact]
    public async Task CreateTask_WithNewTagName_ShouldCreateTagAndAssociate()
    {
        // Arrange
        var taskService = new TaskService(DbContext); // No futuro será injetado com DbContext InMemory
        var newTask = new TaskItem
        {
            Titulo = "Aprender xUnit",
            IsAtivo = true
        };
        var newTagName = "Estudos";

        // Act
        await taskService.CreateTaskAsync(newTask, newTagName);

        // Assert
        var tag = await DbContext.Set<Tag>().FirstOrDefaultAsync(t => t.Nome == newTagName);
        tag.Should().NotBeNull();
        newTask.TagId.Should().Be(tag.Id);
    }
}
