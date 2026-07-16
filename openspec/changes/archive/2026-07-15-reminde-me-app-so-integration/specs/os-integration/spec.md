## ADDED Requirements

### Requirement: Windows Toast Notifications
The system SHALL display native Windows Toast notifications to alert the user about completed Pomodoro sessions or scheduled reminders.

#### Scenario: Send notification when Do Not Disturb is off
- **WHEN** the application triggers a notification and the Windows "Do Not Disturb" (Focus Assist) mode is off
- **THEN** the system displays a Toast notification with the provided title and message

#### Scenario: Suppress notification when Do Not Disturb is on
- **WHEN** the application triggers a notification but the Windows "Do Not Disturb" (Focus Assist) mode is active
- **THEN** the system suppresses the Toast notification or delivers it silently to the Action Center, respecting OS settings.

### Requirement: Application System Tray and Window Lifecycle
The system SHALL maintain a System Tray icon and keep the application running in the background when the main window is closed by the user.

#### Scenario: User closes the main window
- **WHEN** the user clicks the close (X) button on the main application window
- **THEN** the window is hidden/minimized instead of terminating the process, and the application continues running in the background.

#### Scenario: User exits via System Tray
- **WHEN** the user right-clicks the System Tray icon and selects the "Sair" (Quit) option
- **THEN** the system displays a native confirmation dialog, and if confirmed, terminates the application process gracefully.
