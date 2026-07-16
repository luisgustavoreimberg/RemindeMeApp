## ADDED Requirements

### Requirement: Task Listing and Grouping
The system SHALL display tasks grouped by their associated Tags on the Dashboard.

#### Scenario: User views the dashboard
- **WHEN** the user navigates to the Dashboard
- **THEN** they see tasks organized under their respective tag categories.

### Requirement: Inline Task Creation
The system SHALL provide an inline form to quickly add new tasks or reminders.

#### Scenario: User creates a quick reminder
- **WHEN** the user inputs a task title and an optional time in "HH:MM" format
- **THEN** the system creates the task and refreshes the task list asynchronously.

### Requirement: Subtasks Accordion
The system SHALL allow parent tasks to expand and collapse to show subtasks.

#### Scenario: User toggles a parent task
- **WHEN** the user clicks on a parent task
- **THEN** the accordion expands to display the subtasks.

### Requirement: Async Completion
The system SHALL allow users to complete tasks and subtasks via checkboxes without reloading the page.

#### Scenario: User checks off a task
- **WHEN** the user clicks the checkbox for a task
- **THEN** the UI updates immediately and an async request is sent to update the database.
