## ADDED Requirements

### Requirement: Static Time Panel
The Focus Mode time panel SHALL remain static and not move or animate when hovered by the mouse.

#### Scenario: Hovering the time panel
- **WHEN** the user hovers the mouse over the glass time panel
- **THEN** the panel remains completely static with no movement

### Requirement: Pomodoro Timer Logic
The system SHALL implement a Pomodoro timer that alternates between Focus time and Break time.

#### Scenario: Starting Pomodoro
- **WHEN** the user clicks "Pomodoro"
- **THEN** the system initiates a Focus time countdown, followed by a Break time countdown

### Requirement: Timer Counting Direction
The Focus Mode timer SHALL start at 00:00 and increment (count up), or start at the configured time and decrement properly (count down from a positive value). It MUST NOT display negative values.

#### Scenario: Running the timer
- **WHEN** the timer is active
- **THEN** it displays valid time values without negative signs

### Requirement: Timer Pause Behavior
The Focus Mode timer SHALL pause at the current time when the pause button is clicked, and resume from that time when started again.

#### Scenario: Pausing the timer
- **WHEN** the user clicks the pause button
- **THEN** the timer stops at its current value without resetting

### Requirement: Time Configuration
The system SHALL allow users to configure the duration for "Focus Time" and "Break Time".

#### Scenario: Configuring Pomodoro times
- **WHEN** the user clicks "Focus Time" or "Break Time"
- **THEN** a configuration modal or view opens to set the respective duration
