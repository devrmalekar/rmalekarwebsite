window.registerScrollWatcher = (dotnetObj) => {
    window.addEventListener("scroll", () => {
        const sections = document.querySelectorAll("section[id]");
        let active = "hero";

        sections.forEach(sec => {
            const rect = sec.getBoundingClientRect();
            if (rect.top <= 150 && rect.bottom >= 150) {
                active = sec.id;
                //console.log("From js: " + active);
            }
        });

        dotnetObj.invokeMethodAsync("UpdateActiveSection", active);

    });
}