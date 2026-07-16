// Global Fetch Helper with AntiForgeryToken
async function fetchWithAntiForgery(url, options = {}) {
    const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;
    
    if (!options.headers) {
        options.headers = {};
    }
    
    if (token) {
        options.headers['RequestVerificationToken'] = token;
    }
    
    return fetch(url, options);
}

// Do Not Disturb Toggle Setup
document.addEventListener("DOMContentLoaded", () => {
    const dndToggle = document.getElementById("dnd-toggle");
    if (dndToggle) {
        dndToggle.addEventListener("click", async () => {
            // Optimistic UI update
            const icon = dndToggle.querySelector(".dnd-icon");
            let isEnabled = icon.getAttribute("data-original-icon") === "do_not_disturb_on";
            
            // Toggle local state
            isEnabled = !isEnabled;
            icon.setAttribute("data-original-icon", isEnabled ? "do_not_disturb_on" : "do_not_disturb_off");
            icon.textContent = isEnabled ? "do_not_disturb_on" : "do_not_disturb_off";
            icon.style.fontVariationSettings = `'FILL' ${isEnabled ? 1 : 0}`;
            
            // Sync with backend (fire and forget for now, normally we'd handle errors)
            try {
                // Since this is global, we can post to a specific API endpoint or a global Razor Page handler
                // Assuming we will have a handler in a base page or specific API controller
                // await fetchWithAntiForgery('/Index?handler=ToggleDND', {
                //     method: 'POST',
                //     headers: { 'Content-Type': 'application/json' },
                //     body: JSON.stringify({ isEnabled })
                // });
            } catch (e) {
                console.error("Failed to toggle DND", e);
            }
        });
    }
});

// UI helpers for Dashboard
function toggleSubtasks(taskId) {
    const subtasks = document.getElementById(`${taskId}-subtasks`);
    const icon = document.getElementById(`icon-${taskId}`);
    
    if (subtasks && icon) {
        if (subtasks.classList.contains('hidden')) {
            subtasks.classList.remove('hidden');
            subtasks.classList.add('block');
            icon.textContent = 'expand_less';
        } else {
            subtasks.classList.remove('block');
            subtasks.classList.add('hidden');
            icon.textContent = 'expand_more';
        }
    }
}
