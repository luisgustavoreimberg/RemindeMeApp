## Why

The RemindeMeApp currently has a solid foundation with its backend, database, and Photino desktop host fully implemented. The problem now is to deliver the modern desktop user interface that the user will actually interact with. Building this frontend is critical to complete the application, translating the provided design mockups into functional ASP.NET Core Razor Pages connected directly to our existing business logic (`ITaskService` and `ITagService`).

## What Changes

- **Layout & Navigation:** Create a Dark Mode application shell in `Pages/Shared/_Layout.cshtml` incorporating Topbar navigation (Dashboard, Focus, Tags) and a "Do Not Disturb" toggle, strictly following the design tokens and layout from `docs/stitch_remindemeapp_modern_desktop_interface/`.
- **Tasks Dashboard (`Pages/Index.cshtml`):** Build the UI for task management including daily task listing grouped by Tag, inline add-task forms, parent/subtask checkboxes, Tag badges, and an accordion layout for subtasks. The HTML structure and CSS classes must reflect the mockups in `dashboard_remindemeapp`.
- **Focus Mode (`Pages/Focus.cshtml`):** Develop a dedicated view with a prominent clock for Pomodoro and free timers, using the visual references in `modo_foco_remindemeapp` as the baseline.
- **Async Updates:** Implement background JavaScript (`wwwroot/js/timer.js`) using the `fetch` API to trigger Razor Page Handlers without causing full page reloads.

## Capabilities

### New Capabilities
- `tasks-dashboard`: Displaying and managing tasks, subtasks, and tags via Razor PageModels connected to backend services.
- `focus-mode`: Managing the Pomodoro and free timers through the UI, interacting with `TimeTrackerContext`, and syncing state via async fetch requests.
- `app-layout`: The global shell encompassing top navigation and the Do Not Disturb toggle.

### Modified Capabilities
- No existing capabilities are being modified at the requirement level.

## Impact

- **Affected Code:** `Pages/` directory (creation of `Index.cshtml`, `Focus.cshtml`, `Shared/_Layout.cshtml`, etc.) and `wwwroot/` for static assets (`timer.js`, CSS).
- **Architecture:** Reinforces the rule of keeping PageModels strictly as presentation controllers with zero business logic.
- **Systems:** Photino's WebView2 instance will now render these Razor Pages natively as the application UI.
