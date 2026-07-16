using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RemindeMeApp.Backend.Data;
using RemindeMeApp.Shared.Models;
using RemindeMeApp.Shared.Services;

namespace RemindeMeApp.Backend.Services;

public class TimeTrackingService : ITimeTrackerService, IPomodoroService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly TimeProvider _timeProvider;

    public TimeTrackingService(ApplicationDbContext dbContext, TimeProvider timeProvider)
    {
        _dbContext = dbContext;
        _timeProvider = timeProvider;
    }

    public static int ParseTimeToSeconds(string time)
    {
        if (string.IsNullOrWhiteSpace(time) || !TimeSpan.TryParse(time, out var timeSpan))
        {
            throw new ArgumentException("Formato de tempo inválido", nameof(time));
        }
        return (int)timeSpan.TotalSeconds;
    }

    public async Task HandleBootResilienceAsync()
    {
        var activeSessions = await _dbContext.TimerSessions.ToListAsync();

        foreach (var session in activeSessions)
        {
            var now = _timeProvider.GetUtcNow().UtcDateTime;
            var elapsed = (now - session.StartTime).TotalSeconds;

            if (session.Tipo == TimerType.Pomodoro && session.DuracaoEsperadaSegundos.HasValue)
            {
                if (elapsed >= session.DuracaoEsperadaSegundos.Value)
                {
                    // Tempo estourou
                    await AddManualTimeAsync(session.ReferenceId, session.IsSubtask, session.DuracaoEsperadaSegundos.Value);
                    _dbContext.TimerSessions.Remove(session);
                }
            }
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task<TimerSession> StartTrackingAsync(int referenceId, bool isSubtask)
    {
        var session = new TimerSession
        {
            ReferenceId = referenceId,
            IsSubtask = isSubtask,
            StartTime = _timeProvider.GetUtcNow().UtcDateTime,
            Tipo = TimerType.FreeTimer
        };

        _dbContext.TimerSessions.Add(session);
        await _dbContext.SaveChangesAsync();
        return session;
    }

    public async Task StopTrackingAsync(int sessionId)
    {
        var session = await _dbContext.TimerSessions.FindAsync(sessionId);
        if (session != null)
        {
            var elapsed = (int)(_timeProvider.GetUtcNow().UtcDateTime - session.StartTime).TotalSeconds;
            await AddManualTimeAsync(session.ReferenceId, session.IsSubtask, elapsed);
            _dbContext.TimerSessions.Remove(session);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<TimerSession?> GetActiveSessionAsync()
    {
        return await _dbContext.TimerSessions.FirstOrDefaultAsync();
    }

    public async Task AddManualTimeAsync(int referenceId, bool isSubtask, int secondsToAdd)
    {
        if (isSubtask)
        {
            var subtask = await _dbContext.Subtasks.FindAsync(referenceId);
            if (subtask != null)
            {
                subtask.TempoTotalGastoSegundos += secondsToAdd;
                _dbContext.Subtasks.Update(subtask);
            }
        }
        else
        {
            var task = await _dbContext.TaskItems.FindAsync(referenceId);
            if (task != null)
            {
                task.TempoTotalGastoSegundos += secondsToAdd;
                _dbContext.TaskItems.Update(task);
            }
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task<TimerSession> StartFocusAsync(int referenceId, bool isSubtask, int durationSeconds)
    {
        var session = new TimerSession
        {
            ReferenceId = referenceId,
            IsSubtask = isSubtask,
            StartTime = _timeProvider.GetUtcNow().UtcDateTime,
            Tipo = TimerType.Pomodoro,
            DuracaoEsperadaSegundos = durationSeconds
        };

        _dbContext.TimerSessions.Add(session);
        await _dbContext.SaveChangesAsync();
        return session;
    }

    public async Task<TimerSession> StartBreakAsync(int durationSeconds)
    {
        var session = new TimerSession
        {
            ReferenceId = 0, // 0 for break
            IsSubtask = false,
            StartTime = _timeProvider.GetUtcNow().UtcDateTime,
            Tipo = TimerType.Pomodoro,
            DuracaoEsperadaSegundos = durationSeconds
        };

        _dbContext.TimerSessions.Add(session);
        await _dbContext.SaveChangesAsync();
        return session;
    }

    public async Task StopAsync(int sessionId)
    {
        await StopTrackingAsync(sessionId);
    }

    public async Task PauseTrackingAsync(int sessionId)
    {
        var session = await _dbContext.TimerSessions.FindAsync(sessionId);
        if (session != null && !session.IsPaused)
        {
            session.IsPaused = true;
            session.PausedElapsedSeconds = (int)(_timeProvider.GetUtcNow().UtcDateTime - session.StartTime).TotalSeconds;
            _dbContext.TimerSessions.Update(session);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task ResumeTrackingAsync(int sessionId)
    {
        var session = await _dbContext.TimerSessions.FindAsync(sessionId);
        if (session != null && session.IsPaused)
        {
            session.IsPaused = false;
            session.StartTime = _timeProvider.GetUtcNow().UtcDateTime.AddSeconds(-session.PausedElapsedSeconds);
            session.PausedElapsedSeconds = 0;
            _dbContext.TimerSessions.Update(session);
            await _dbContext.SaveChangesAsync();
        }
    }
}
