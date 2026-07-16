## Context

The backend, database, and Photino desktop wrapper are implemented for RemindeMeApp. The final step is building the frontend using ASP.NET Core Razor Pages. Since this application runs locally wrapped in Photino, we have the flexibility of full web technologies but must ensure a snappy, app-like experience. This requires fetching data and updating timers asynchronously rather than relying on full-page reloads, while strictly maintaining a Dark Mode aesthetic.

## Goals / Non-Goals

**Goals:**
- Implement a Dark Mode main layout (`_Layout.cshtml`) featuring a top navigation bar and "Do Not Disturb" toggle.
- Create the Tasks Dashboard (`Index.cshtml`) with task lists grouped by tags, inline creation forms, and subtask accordions.
- Build the Focus Mode (`Focus.cshtml`) interface with a prominent visual timer and task association dropdown.
- Ensure timers update seamlessly via JavaScript (`fetch` API) communicating with Razor Page Handlers without refreshing the UI.
- Wire up Razor PageModels with dependency injection for `ITaskService` and `ITagService`.

**Non-Goals:**
- Introducing business logic into the Razor PageModels.
- Creating a SPA (Single Page Application) using React, Vue, or Angular; the UI will be Razor Pages + vanilla JS.
- Modifying the existing EF Core database schema or backend logic.

## Decisions

**1. Razor Pages + Vanilla JS (`fetch`) for Interactivity**
*Rationale:* ASP.NET Core Razor Pages is the chosen framework. To achieve a desktop-like feel without full page reloads for timers and quick actions, we will use vanilla JavaScript's `fetch` API to call specific `OnPost...Async` and `OnGet...Async` handlers in the PageModels.
*Trade-off:* Managing UI state manually in JS is more error-prone than using a reactive SPA framework, but it keeps the stack simple and aligned with the architectural constraints.

**2. Styling Approach: Strict Adherence to Stitch Mockups**
*Rationale:* The UI must look exactly like the mockups provided in `docs/stitch_remindemeapp_modern_desktop_interface/`. We will extract the exact HTML structure and CSS styles directly from those `code.html` files, migrating them into ASP.NET Core Razor structures and static CSS files (e.g., `wwwroot/css/site.css`), preserving the Dark Mode theme, custom fonts, and exact visual specifications.
*Trade-off:* We will need to adapt hardcoded static HTML lists and timers into dynamic Razor constructs, which requires careful mapping to ensure no visual fidelity is lost during the conversion.

**3. Dependency Injection in PageModels**
*Rationale:* PageModels will inject `ITaskService` and `ITagService` in their constructors. The handlers will solely orchestrate calls to these services and return `PageResult`, `PartialViewResult`, or `JsonResult` depending on whether it's a standard navigation or an async `fetch` request.

## Risks / Trade-offs

- **[Risk] State Synchronization:** Since timers and tasks are updated asynchronously, the UI state in JS could drift from the server state.
  *Mitigation:* The frontend JS will periodically sync with the backend (e.g., via the `UpdateClock` handler) to fetch the true elapsed time, resolving any drift caused by browser throttling or sleep states.

- **[Risk] Anti-Forgery Token Validation with Fetch:** ASP.NET Core requires anti-forgery tokens for POST requests.
  *Mitigation:* The `timer.js` script will extract the token from the standard Razor generated form or meta tag and append it to the `RequestVerificationToken` header of all `fetch` requests.
