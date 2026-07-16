## ADDED Requirements

### Requirement: Pomodoro and Free Timer Execution
The system SHALL provide a large visual clock to execute Pomodoro or free timers.

#### Scenario: User starts a focus session
- **WHEN** the user clicks the play button on the timer
- **THEN** the timer begins counting down or up, and the state is persisted to the backend via an async request.

### Requirement: Timer Association
The system SHALL allow the user to associate the active timer with a specific task via a dropdown.

#### Scenario: User links a task to a focus session
- **WHEN** the user selects a task from the active tasks dropdown before starting the timer
- **THEN** the accumulated time is added to that specific task's total tracked time upon completion.

### Requirement: Background Synchronization
The system SHALL synchronize the timer state with the server to prevent drift and ensure persistence.

#### Scenario: Timer runs over time
- **WHEN** the timer is running
- **THEN** the frontend JavaScript fetches updates from the Razor Page handler to ensure the displayed time matches the true elapsed time.
