using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RemindeMeApp.Shared.Models;
using RemindeMeApp.Shared.Services;

namespace RemindeMeApp.Frontend.Pages;

public class FocusModel : PageModel
{
    private readonly ITaskService _taskService;
    private readonly ITimeTrackerService _trackerService;
    private readonly IPomodoroService _pomodoroService;

    public FocusModel(ITaskService taskService, ITimeTrackerService trackerService, IPomodoroService pomodoroService)
    {
        _taskService = taskService;
        _trackerService = trackerService;
        _pomodoroService = pomodoroService;
    }

    public IEnumerable<TaskItem> ActiveTasks { get; set; } = new List<TaskItem>();
    
    [BindProperty]
    public int? SelectedTaskId { get; set; }
    
    public TimerSession? ActiveSession { get; set; }

    public async Task OnGetAsync()
    {
        // Get all tasks to populate the dropdown
        var allTasks = await _taskService.GetAllAsync();
        ActiveTasks = allTasks.Where(t => t.IsAtivo);
        
        // Get active session if any
        ActiveSession = await _trackerService.GetActiveSessionAsync();
        if (ActiveSession != null)
        {
            SelectedTaskId = ActiveSession.ReferenceId;
        }
    }

    public async Task<IActionResult> OnPostStartTimerAsync(int? taskId)
    {
        if (taskId.HasValue)
        {
            await _trackerService.StartTrackingAsync(taskId.Value, isSubtask: false);
        }
        return new JsonResult(new { success = true });
    }

    public async Task<IActionResult> OnPostStartPomodoroAsync(int? taskId, int durationSeconds)
    {
        if (taskId.HasValue)
        {
            await _pomodoroService.StartFocusAsync(taskId.Value, isSubtask: false, durationSeconds);
        }
        return new JsonResult(new { success = true });
    }

    public async Task<IActionResult> OnPostStartBreakAsync(int durationSeconds)
    {
        await _pomodoroService.StartBreakAsync(durationSeconds);
        return new JsonResult(new { success = true });
    }

    public async Task<IActionResult> OnPostStopTimerAsync()
    {
        var active = await _trackerService.GetActiveSessionAsync();
        if (active != null)
        {
            await _trackerService.StopTrackingAsync(active.Id);
        }
        return new JsonResult(new { success = true });
    }

    public async Task<IActionResult> OnPostPauseTimerAsync()
    {
        var active = await _trackerService.GetActiveSessionAsync();
        if (active != null)
        {
            await _trackerService.PauseTrackingAsync(active.Id);
        }
        return new JsonResult(new { success = true });
    }

    public async Task<IActionResult> OnPostResumeTimerAsync()
    {
        var active = await _trackerService.GetActiveSessionAsync();
        if (active != null)
        {
            await _trackerService.ResumeTrackingAsync(active.Id);
        }
        return new JsonResult(new { success = true });
    }

    public async Task<IActionResult> OnGetUpdateClockAsync()
    {
        var active = await _trackerService.GetActiveSessionAsync();
        if (active == null) 
        {
            return new JsonResult(new { isActive = false, isPaused = false, elapsedSeconds = 0, isPomodoro = false, remainingSeconds = 0 });
        }
        
        var elapsed = active.IsPaused ? active.PausedElapsedSeconds : (DateTime.UtcNow - active.StartTime).TotalSeconds;
        var isPomodoro = active.Tipo == TimerType.Pomodoro && active.DuracaoEsperadaSegundos.HasValue;
        var remaining = isPomodoro ? (active.DuracaoEsperadaSegundos!.Value - elapsed) : 0;

        return new JsonResult(new { 
            isActive = true, 
            isPaused = active.IsPaused,
            elapsedSeconds = (int)elapsed,
            isPomodoro = isPomodoro,
            remainingSeconds = (int)Math.Max(0, remaining)
        });
    }
}
