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

public class TimerResilienceTests : TestBase
{
    [Fact]
    public async Task HandleBootResilience_WithRunningFreeTimer_ShouldCalculateTranscurredTime()
    {
        // Arrange
        var timeTrackingService = new TimeTrackingService(DbContext, TimeProviderMock.Object);
        var task = new TaskItem { Titulo = "Task 1" };
        DbContext.TaskItems.Add(task);
        await DbContext.SaveChangesAsync();

        var startTime = TimeProviderMock.Object.GetUtcNow().UtcDateTime.AddMinutes(-10); // 10 mins running
        var session = new TimerSession
        {
            ReferenceId = task.Id,
            IsSubtask = false,
            StartTime = startTime,
            Tipo = TimerType.FreeTimer
        };
        DbContext.TimerSessions.Add(session);
        await DbContext.SaveChangesAsync();

        // Act
        await timeTrackingService.HandleBootResilienceAsync();

        // Assert
        var activeSession = await DbContext.TimerSessions.FirstOrDefaultAsync();
        activeSession.Should().NotBeNull("FreeTimer deve continuar rodando");
        activeSession!.Id.Should().Be(session.Id);
    }

    [Fact]
    public async Task HandleBootResilience_WithExpiredPomodoro_ShouldCompleteAndClearContext()
    {
        // Arrange
        var timeTrackingService = new TimeTrackingService(DbContext, TimeProviderMock.Object);
        var task = new TaskItem { Titulo = "Task Pomodoro" };
        DbContext.TaskItems.Add(task);
        await DbContext.SaveChangesAsync();

        var duration = 1500; // 25 minutes
        var startTime = TimeProviderMock.Object.GetUtcNow().UtcDateTime.AddMinutes(-30); // 30 mins running (expired)
        
        var session = new TimerSession
        {
            ReferenceId = task.Id,
            IsSubtask = false,
            StartTime = startTime,
            Tipo = TimerType.Pomodoro,
            DuracaoEsperadaSegundos = duration
        };
        DbContext.TimerSessions.Add(session);
        await DbContext.SaveChangesAsync();

        // Act
        await timeTrackingService.HandleBootResilienceAsync();

        // Assert
        var activeSession = await DbContext.TimerSessions.FirstOrDefaultAsync();
        activeSession.Should().BeNull("Pomodoro expirado deve ser limpo");

        var updatedTask = await DbContext.TaskItems.FindAsync(task.Id);
        updatedTask!.TempoTotalGastoSegundos.Should().Be(duration, "O tempo esperado deve ser somado à tarefa");
    }

    [Fact]
    public async Task HandleBootResilience_WithRunningPomodoro_ShouldResumeTimer()
    {
        // Arrange
        var timeTrackingService = new TimeTrackingService(DbContext, TimeProviderMock.Object);
        var task = new TaskItem { Titulo = "Task Pomodoro Running" };
        DbContext.TaskItems.Add(task);
        await DbContext.SaveChangesAsync();

        var duration = 1500; // 25 minutes
        var startTime = TimeProviderMock.Object.GetUtcNow().UtcDateTime.AddMinutes(-10); // 10 mins running (not expired)
        
        var session = new TimerSession
        {
            ReferenceId = task.Id,
            IsSubtask = false,
            StartTime = startTime,
            Tipo = TimerType.Pomodoro,
            DuracaoEsperadaSegundos = duration
        };
        DbContext.TimerSessions.Add(session);
        await DbContext.SaveChangesAsync();

        // Act
        await timeTrackingService.HandleBootResilienceAsync();

        // Assert
        var activeSession = await DbContext.TimerSessions.FirstOrDefaultAsync();
        activeSession.Should().NotBeNull("Pomodoro ainda não expirado deve continuar rodando");
    }
}
