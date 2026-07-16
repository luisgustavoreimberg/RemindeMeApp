## 1. Window Management
- [x] 1.1 Remove custom HTML close, minimize, and maximize buttons from the UI layout.
- [x] 1.2 Configure desktop host window settings to enforce minimum width and height constraints.
- [x] 1.3 Intercept window close event in the desktop host and modify it to minimize the window to the system tray instead.

## 2. Sidebar Menu
- [x] 2.1 Implement navigation or click handlers for the "Tags" sidebar menu item.
- [x] 2.2 Implement navigation or click handlers for the "Settings" sidebar menu item.

## 3. Dashboard Features
- [ ] 3.1 Implement tag/task filtering logic (Name, Elapsed Time, Completion Date) in the dashboard component state.
- [ ] 3.2 Implement tag/task sorting logic (Alphabetical, Completion Date, Elapsed Time) in the dashboard component state.
- [ ] 3.3 Add click events to the date and tag icons in the Quick Task input to open their respective selection popups.
- [ ] 3.4 Create an Edit Task view/modal and connect it to existing tasks in the dashboard.
- [ ] 3.5 Implement UI and logic to create subtasks from a parent task.
- [ ] 3.6 Update the dashboard task list to render subtasks hierarchically under their parent tasks.

## 4. Focus Mode Mechanics
- [ ] 4.1 Update CSS for the time panel's glass div to remove hover movement/animations.
- [ ] 4.2 Fix the duplicated task selection field (remove duplicate arrows/fields) in the Focus Mode UI.
- [ ] 4.3 Create configuration modal for "Focus Time" and "Break Time".
- [ ] 4.4 Hook up the "Focus Time" and "Break Time" buttons to open the new configuration modal.
- [ ] 4.5 Fix timer state logic so the timer increments from 00:00 (or decrements from a positive value) instead of showing negative numbers.
- [ ] 4.6 Fix pause button logic to persist current timer state instead of resetting it.
- [ ] 4.7 Hook up the "Pomodoro" button to initiate the configured Focus and Break time sequence.
