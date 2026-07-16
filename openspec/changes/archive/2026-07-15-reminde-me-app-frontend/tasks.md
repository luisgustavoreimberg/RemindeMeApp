## 1. Global Setup & Layout

- [x] 1.1 Create `wwwroot/css/site.css` by extracting the base Dark Mode CSS variables and typography from `docs/stitch_remindemeapp_modern_desktop_interface/`.
- [x] 1.2 Implement the Main Shell in `Pages/Shared/_Layout.cshtml` with Topbar navigation (Dashboard, Focus, Tags), copying the structural HTML and classes from the mockups.
- [x] 1.3 Add the "Do Not Disturb" toggle UI and a backend handler to persist the preference.
- [x] 1.4 Create `wwwroot/js/timer.js` to manage `fetch` API requests, including anti-forgery token headers.

## 2. Tasks Dashboard UI (`Index.cshtml`)

- [x] 2.1 Create `Pages/Index.cshtml` and `Pages/Index.cshtml.cs` (PageModel).
- [x] 2.2 Inject `ITaskService` and `ITagService` into `Index.cshtml.cs` and bind data for tasks grouped by Tags.
- [x] 2.3 Build the task listing UI, extracting HTML and CSS from `dashboard_remindemeapp/code.html`, including parent/subtask checkboxes and dynamic Tag badges.
- [x] 2.4 Implement CSS/JS for the accordion functionality to expand and collapse subtasks, preserving the mockup's visual behavior.
- [x] 2.5 Build the inline "Add Task" form with a time input (HH:MM format) using Tag Helpers.
- [x] 2.6 Wire up asynchronous completion (checkboxes) and task creation using `timer.js` fetch calls.

## 3. Focus Mode UI (`Focus.cshtml`)

- [x] 3.1 Create `Pages/Focus.cshtml` and `Pages/Focus.cshtml.cs`.
- [x] 3.2 Create a dropdown populated with active tasks for timer association using `ITaskService`.
- [x] 3.3 Build the main visual clock interface for Pomodoro and Free timers, directly implementing the HTML/CSS from `modo_foco_remindemeapp/code.html`.
- [x] 3.4 Implement Razor Page Handlers: `OnPostStartTimerAsync()`, `OnPostPauseTimerAsync()`, and `OnPostStopTimerAsync()`.
- [x] 3.5 Setup periodic synchronization in `timer.js` to fetch `?handler=UpdateClock` and update the UI.
