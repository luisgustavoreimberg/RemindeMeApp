## ADDED Requirements

### Requirement: Global Dark Mode Shell
The system SHALL render all UI components within a strict Dark Mode Layout.

#### Scenario: User loads the application
- **WHEN** the application starts within the Photino shell
- **THEN** the rendered HTML respects the Dark Mode CSS variables and layout structure.

### Requirement: Top Navigation
The system SHALL provide navigation links to Dashboard, Focus, and Tags in a top bar.

#### Scenario: User navigates between views
- **WHEN** the user clicks a link in the top bar
- **THEN** the system routes the user to the corresponding Razor Page.

### Requirement: Do Not Disturb Toggle
The system SHALL provide a global toggle to enable or disable Do Not Disturb mode.

#### Scenario: User toggles Do Not Disturb
- **WHEN** the user clicks the Do Not Disturb switch
- **THEN** an async request saves this preference to the backend to suppress native notifications.
