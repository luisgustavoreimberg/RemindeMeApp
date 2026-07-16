## 1. Setup

- [x] 1.1 Install Entity Framework Core SQLite and Design packages in the Backend project.
- [x] 1.2 Implement DbContext (`ApplicationDbContext`) with DbSets for `Task`, `Subtask`, `Tag`, and `TimeTrackerContext`.

## 2. Models / Entities Configuration

- [x] 2.1 Update models (`Task`, `Subtask`, `Tag`, `TimeTrackerContext`) with necessary data annotations or Fluent API configuration for EF Core constraints.
- [x] 2.2 Create initial EF Core Migration and apply it to create the SQLite local database.

## 3. Services - Task Management

- [x] 3.1 Implement `TaskService` (CRUD for Tasks, Subtasks).
- [x] 3.2 Implement cascade completion/deletion logic (Tasks -> Subtasks) and Tag association/detachment.
- [x] 3.3 Implement inline Tag creation during Task creation.
- [x] 3.4 Verify and make sure all related Task tests (like `CascadeOperationsTests`) pass.

## 4. Services - Time Tracking & Resilience

- [x] 4.1 Implement `TimeTrackingService` with Free Tracking and Pomodoro tracking logic.
- [x] 4.2 Implement `TimeTrackerContext` saving on timer start and boot recovery logic for resilience.
- [x] 4.3 Implement manual time insertion (parsing HH:MM into seconds).
- [x] 4.4 Verify and make sure all related tests (like `TimerResilienceTests`, `TimeParsingTests`) pass.

## 5. Integration

- [x] 5.1 Configure the Dependency Injection (DI) container to register `ApplicationDbContext` (Scoped) and the implementation of `ITaskService` and `ITimeTrackingService`.
