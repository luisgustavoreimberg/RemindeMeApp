using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RemindeMeApp.Shared.Models;
using RemindeMeApp.Shared.Services;

namespace RemindeMeApp.Frontend.Pages;

public class IndexModel : PageModel
{
    private readonly ITaskService _taskService;
    private readonly ITagService _tagService;

    public IndexModel(ITaskService taskService, ITagService tagService)
    {
        _taskService = taskService;
        _tagService = tagService;
    }

    public IEnumerable<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    public IEnumerable<Tag> Tags { get; set; } = new List<Tag>();

    [BindProperty]
    public string NewTaskTitle { get; set; } = string.Empty;

    public async Task OnGetAsync()
    {
        Tasks = await _taskService.GetAllAsync();
        Tags = await _tagService.GetAllAsync();
    }

    public async Task<IActionResult> OnPostToggleTaskAsync(int id)
    {
        await _taskService.ToggleActiveAsync(id);
        return new JsonResult(new { success = true });
    }

    public async Task<IActionResult> OnPostQuickAddAsync()
    {
        if (!string.IsNullOrWhiteSpace(NewTaskTitle))
        {
            await _taskService.CreateAsync(new TaskItem 
            { 
                Titulo = NewTaskTitle,
                IsAtivo = true 
            });
        }
        
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostEditTaskAsync(int id, string title)
    {
        var task = await _taskService.GetByIdAsync(id);
        if (task != null && !string.IsNullOrWhiteSpace(title))
        {
            task.Titulo = title;
            await _taskService.UpdateAsync(task);
        }
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostAddSubtaskAsync(int parentId, string title)
    {
        var task = await _taskService.GetByIdAsync(parentId);
        if (task != null && !string.IsNullOrWhiteSpace(title))
        {
            task.Subtasks.Add(new Subtask { Titulo = title, IsAtivo = true });
            await _taskService.UpdateAsync(task);
        }
        return RedirectToPage();
    }
}
