let skillObserver;
if (!skillObserver) {
    skillObserver = new IntersectionObserver(entries => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add("visible");
                skillObserver.unobserve(entry.target);
            }
        });
    });
}

const watchSkillFadeElements = () => {
    document.querySelectorAll(".fade-skill:not([data-observed])")
        .forEach(el => {
            el.dataset.observed = "true";
            skillObserver.observe(el);
        });
}

watchSkillFadeElements();

new MutationObserver(watchSkillFadeElements).observe(document.body, {
    childList: true,
    subtree: true
});

