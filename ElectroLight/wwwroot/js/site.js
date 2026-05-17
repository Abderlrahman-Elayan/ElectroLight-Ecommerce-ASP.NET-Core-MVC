document.addEventListener("DOMContentLoaded", function () {

    const toggleBtn = document.getElementById("themeToggle");
    const isAdmin = document.body.classList.contains("admin-page");

    if (!isAdmin) {
        document.documentElement.setAttribute("data-bs-theme", "light");
        return;
    }

    function setTheme(theme) {
        document.documentElement.setAttribute("data-bs-theme", theme);
        localStorage.setItem("theme", theme);

        if (toggleBtn) {
            toggleBtn.innerHTML =
                theme === "dark"
                    ? '<i class="bi bi-sun-fill"></i>'
                    : '<i class="bi bi-moon-fill"></i>';
        }
    }

    let savedTheme = localStorage.getItem("theme") || "dark";
    setTheme(savedTheme);

    if (toggleBtn) {
        toggleBtn.addEventListener("click", function () {
            let currentTheme = document.documentElement.getAttribute("data-bs-theme");
            setTheme(currentTheme === "dark" ? "light" : "dark");
        });
    }

});

    async function handleEmailClick(event) {

        event.preventDefault();

    const email = "abdelrahmanelayanformal@gmail.com";

    window.location.href = `mailto:${email}`;

        // Fallback: copy email after short delay
        setTimeout(async () => {

        try {

        await navigator.clipboard.writeText(email);

    const message = document.getElementById("copy-message");

    message.classList.add("show");

        setTimeout(() => {
        message.classList.remove("show");
        }, 2500);

        }
    catch (err) {

        console.error("Clipboard copy failed");

        }

        }, 800);
        }

