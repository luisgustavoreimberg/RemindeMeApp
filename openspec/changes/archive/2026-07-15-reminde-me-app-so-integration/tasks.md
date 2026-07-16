## 1. Interoperability Classes

- [x] 1.1 Create `INotificationManager` and implement `NotificationManager` using a suitable Windows Toast API (e.g., `CommunityToolkit.WinUI.Notifications` or Windows SDK), including logic to check "Do Not Disturb" (Focus Assist).
- [x] 1.2 Create `ITrayIconManager` and implement `TrayIconManager` to encapsulate System Tray logic.

## 2. Application Host Configuration

- [x] 2.1 Update `Program.cs` to configure Kestrel to run on a random available port (e.g., port 0).
- [x] 2.2 Configure full Dependency Injection (DI) in `Program.cs` for `AppDbContext`, domain services (`ITaskService`, `ITimeTrackingService`, etc.), and interoperability managers (`INotificationManager`, `ITrayIconManager`).
- [x] 2.3 Configure `PhotinoWindow` initialization, setting the title to "RemindeMeApp" and pointing the Webview to the dynamic Kestrel port.

## 3. Window Lifecycle and System Tray

- [x] 3.1 Hook into `PhotinoWindow.WindowClosing` event to cancel the default exit behavior, instead minimizing or hiding the window.
- [x] 3.2 Configure the System Tray icon and add a context menu with a "Sair" (Quit) option.
- [x] 3.3 Implement a native Photino confirmation dialog that appears when "Sair" is clicked, and gracefully terminate the Kestrel/Photino process only if the user confirms.
