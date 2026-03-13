// Theme Toggle Script

document.addEventListener("DOMContentLoaded", function () {

    const toggleBtn = document.getElementById("themeToggle");

    // function to apply theme
    function setTheme(theme) {
        document.documentElement.setAttribute("data-bs-theme", theme);
        localStorage.setItem("theme", theme);

        // change icon
        if (toggleBtn) {
            toggleBtn.innerHTML =
                theme === "dark"
                    ? '<i class="bi bi-sun-fill"></i>'
                    : '<i class="bi bi-moon-fill"></i>';
        }
    }

    // load saved theme
    let savedTheme = localStorage.getItem("theme") || "dark";
    setTheme(savedTheme);

    // toggle on button click
    if (toggleBtn) {
        toggleBtn.addEventListener("click", function () {

            let currentTheme = document.documentElement.getAttribute("data-bs-theme");

            if (currentTheme === "dark") {
                setTheme("light");
            } else {
                setTheme("dark");
            }

        });
    }

});