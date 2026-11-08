const sidebar = document.getElementById("app-sidebar");
const sidebarToggle = document.querySelector(".sidebar-toggle");

if (sidebar && sidebarToggle) {
    sidebarToggle.addEventListener("click", () => {
        const isOpen = sidebar.classList.toggle("sidebar-open");
        sidebarToggle.setAttribute("aria-expanded", String(isOpen));
    });

    document.addEventListener("click", (event) => {
        if (
            sidebar.classList.contains("sidebar-open")
            && !sidebar.contains(event.target)
            && !sidebarToggle.contains(event.target)
        ) {
            sidebar.classList.remove("sidebar-open");
            sidebarToggle.setAttribute("aria-expanded", "false");
        }
    });
}
