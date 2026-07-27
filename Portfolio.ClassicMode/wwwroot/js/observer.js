const observer = new IntersectionObserver(entries => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            entry.target.classList.add("visible");
        }
        else {
            entry.target.classList.remove("visible");
        }
    });
});

const watchFadeElements = () => {
    document.querySelectorAll(".fade").forEach(el => observer.observe(el));
};

watchFadeElements();

new MutationObserver(watchFadeElements).observe(document.body, {
    childList: true,
    subtree: true
})
