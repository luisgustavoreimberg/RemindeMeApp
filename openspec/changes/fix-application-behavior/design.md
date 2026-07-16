## Context

The application consists of a desktop host and a web frontend. Testing revealed gaps in window management logic (missing system tray minimize, sizing constraints), non-functional dashboard actions (filtering, sorting, tag assignment), and incomplete focus mode mechanics (timer logic issues, missing config modals).

## Goals / Non-Goals

**Goals:**
- Properly integrate the frontend with the desktop application shell for window management (minimize to system tray, apply sizing constraints).
- Make the dashboard filter, sorting, and task modification fully functional.
- Implement correct timer logic, configuration hooks, and state management for Pomodoro in Focus Mode.

**Non-Goals:**
- Redesigning the entire UI theme.
- Changing the underlying architecture or framework.

## Decisions

- **Window Management**: 
  - *Decision*: Remove custom HTML close/minimize/maximize buttons and configure the native host window to have a minimum width/height and hide to system tray on close.
  - *Rationale*: Native OS controls provide a more consistent experience and better accessibility. Hiding to tray ensures background processes (like the Pomodoro timer) can continue.
- **Dashboard Filtering and Sorting**:
  - *Decision*: Implement frontend-side filtering and sorting for tasks/tags using array manipulation logic in the state layer.
  - *Rationale*: Keeps the UI responsive without needing round-trips to the backend, ideal for local-first desktop apps.
- **Focus Mode Timer**:
  - *Decision*: Fix timer state logic to count up from 00:00 (or down from a positive value if configured) instead of starting negative. Maintain state correctly upon pausing without resetting. 
  - *Rationale*: The current negative countdown is a bug. Proper pause/resume is critical for a usable Pomodoro timer.
- **Subtasks & Editing**:
  - *Decision*: Add an edit view for tasks and a hierarchical view for subtasks, maintaining a parent-child relationship in the task data model.

## Risks / Trade-offs

- **[Risk]** Handling system tray and native window operations may require platform-specific code in the .NET desktop host. 
  **[Mitigation]** Use existing .NET host window configuration APIs (e.g., Photino or WPF) and handle the form closing event to cancel and minimize instead.
- **[Risk]** State loss in Pomodoro timer if the user navigates away from the Focus Mode page.
  **[Mitigation]** Ensure timer state is held in a global state service or backend, rather than component local state.
