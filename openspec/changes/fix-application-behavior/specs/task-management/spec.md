## ADDED Requirements

### Requirement: Task Editing
The system SHALL allow users to modify the parameters (e.g., name, tags, date) of an existing task.

#### Scenario: Editing a task
- **WHEN** the user selects the edit option on a task
- **THEN** the system opens a view allowing the user to modify and save the task's parameters

### Requirement: Subtask Creation
The system SHALL allow users to create subtasks under a parent task.

#### Scenario: Creating a subtask
- **WHEN** the user selects the option to add a subtask to an existing task
- **THEN** the system creates a new subtask associated with the parent task

### Requirement: Subtask Listing
The system SHALL display subtasks in a hierarchical list under their respective parent tasks.

#### Scenario: Viewing subtasks
- **WHEN** a task has subtasks
- **THEN** the system displays the subtasks visually nested under the parent task
