const sidebar = document.getElementById("app-sidebar");
const sidebarToggle = document.querySelector(".sidebar-toggle");

// Abre e fecha a navegação móvel e mantém aria-expanded sincronizado para acessibilidade.
if (sidebar && sidebarToggle) {
    sidebarToggle.addEventListener("click", () => {
        const isOpen = sidebar.classList.toggle("sidebar-open");
        sidebarToggle.setAttribute("aria-expanded", String(isOpen));
    });

    // Fecha o menu ao tocar fora dele, sem interferir nos cliques dentro da navegação.
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
