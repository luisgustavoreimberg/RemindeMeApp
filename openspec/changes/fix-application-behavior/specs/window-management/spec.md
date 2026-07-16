## ADDED Requirements

### Requirement: Minimize to System Tray
The application SHALL minimize to the system tray instead of exiting when the close button is clicked.

#### Scenario: Closing the application
- **WHEN** the user clicks the native close button on the window frame
- **THEN** the application hides its window and appears only in the system tray

### Requirement: Native Window Controls
The application SHALL use native OS window controls and not display custom HTML-based close, minimize, or maximize icons.

#### Scenario: Viewing the window frame
- **WHEN** the application is open
- **THEN** the user sees only the native OS window controls and no custom HTML controls

### Requirement: Minimum Window Size
The application SHALL enforce a minimum window width and height to prevent extreme UI distortion.

#### Scenario: Resizing the window
- **WHEN** the user attempts to resize the window below the minimum threshold
- **THEN** the window size stops decreasing at the configured minimum width and height
