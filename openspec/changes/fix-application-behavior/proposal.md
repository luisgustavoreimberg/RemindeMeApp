## Why

During testing, several behavioral, functional, and UI issues were identified across the application, specifically in window management, dashboard functionality, and focus mode mechanics. Resolving these is necessary to ensure the application works as intended and provides a proper user experience.

## What Changes

- **Window Management**: Modify close behavior to minimize the app to the system tray instead of closing it completely. Remove custom close, minimize, and maximize icons from the UI to rely strictly on the system's native window frame. Implement a minimum width and height constraint for the application window.
- **Sidebar Menu**: Implement navigation/action for "Tags" and "Settings" menu items.
- **Dashboard**: 
  - Fix Tag sorting (A-Z/Z-A, Completion Date, Elapsed Time) and filtering (Name, Elapsed Time, Completion Date).
  - Activate date and tag icons in Quick Task creation to open corresponding selection popups.
  - Add an edit view to modify parameters of existing tasks.
  - Implement subtask creation and hierarchical listing under parent tasks.
- **Focus Mode**:
  - Make the time panel's glass div static (remove hover movement).
  - Fix the duplicated task selection field/arrows.
  - Implement the "Pomodoro" mode logic (Focus and Break time).
  - Correct timer behavior: start at 00:00 and increment, instead of counting down from negative values.
  - Fix the pause button so it pauses the timer instead of resetting and restarting it.
  - Enable "Focus Time" and "Break Time" configuration settings.

## Capabilities

### New Capabilities
- `window-management`: Handling window minimize to tray, window size limits, and native frame setup.
- `task-management`: Editing existing tasks and subtask creation/listing.
- `focus-mode`: Pomodoro timer behavior (focus/break cycles), timer counting logic, and pause functionality.
- `dashboard-filtering`: Sorting and filtering tasks/tags on the dashboard.

### Modified Capabilities


## Impact

- **Frontend/UI Components**: Sidebar, Dashboard, Focus Mode layout and inputs.
- **State Management / App Logic**: Timer logic, sorting/filtering logic, task/subtask structure.
- **Desktop/Electron/Tauri Shell**: Window constraints and close-to-tray mechanics (given the requirement to minimize to system tray).
