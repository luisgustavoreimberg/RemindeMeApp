using System;
using System.Threading.Tasks;
using FluentAssertions;
using RemindeMeApp.Backend.Services;
using RemindeMeApp.Shared.Models;
using RemindeMeApp.Tests.Infrastructure;
using Xunit;

namespace RemindeMeApp.Tests.BusinessRules;

public class ValidationTests : TestBase
{
    [Fact]
    public async Task CreateTask_WithoutTitle_ThrowsArgumentException()
    {
        // Arrange
        var taskService = new TaskService(DbContext); // Em um cenário real, as dependências (DbContext) seriam injetadas
        var newTask = new TaskItem
        {
            Titulo = string.Empty // Inválido
        };

        // Act
        Func<Task> act = async () => await taskService.CreateTaskAsync(newTask);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("*Título é obrigatório*");
    }

    [Fact]
    public async Task CreateSubtask_WithoutParentId_ThrowsArgumentException()
    {
        // Arrange
        var taskService = new TaskService(DbContext);
        var newSubtask = new Subtask
        {
            Titulo = "Subtarefa Válida",
            ParentTaskId = 0 // Inválido, deve ser FK válida
        };

        // Act
        Func<Task> act = async () => await taskService.CreateSubtaskAsync(newSubtask);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("*ParentTaskId é obrigatório*");
    }
}
