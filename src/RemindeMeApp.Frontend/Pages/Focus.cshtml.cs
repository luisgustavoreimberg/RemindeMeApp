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

    public FocusModel(ITaskService taskService, ITimeTrackerService trackerService)
    {
        _taskService = taskService;
        _trackerService = trackerService;
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

    public async Task<IActionResult> OnPostStopTimerAsync()
    {
        var active = await _trackerService.GetActiveSessionAsync();
        if (active != null)
        {
            await _trackerService.StopTrackingAsync(active.Id);
        }
        return new JsonResult(new { success = true });
    }

    public async Task<IActionResult> OnGetUpdateClockAsync()
    {
        var active = await _trackerService.GetActiveSessionAsync();
        if (active == null) 
        {
            return new JsonResult(new { isActive = false, elapsedSeconds = 0 });
        }
        
        var elapsed = (DateTime.Now - active.StartTime).TotalSeconds;
        return new JsonResult(new { isActive = true, elapsedSeconds = (int)elapsed });
    }
}
