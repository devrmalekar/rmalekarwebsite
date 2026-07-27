let parallaxAttached = false;
const isMobile = window.innerWidth < 768;

//Throttle scroll for smoothness
let ticking = false;

window.parallax = {
    init: function () {
        if (!parallaxAttached) {
            window.addEventListener("scroll", () => this.refresh());
            parallaxAttached = true;
        }

        const observer = new MutationObserver(() => {
            // parallaxAttached = false;
            this.refresh();
        });
        document.querySelectorAll(".parallax").forEach(el => {
            observer.observe(el, {
                attributes: true
            })
        })
    },

    refresh() {
        if (isMobile) return; // disable on mobile

        if (!ticking) {
            window.requestAnimationFrame(() => {
                const scrollY = window.scrollY;
                const elements = document.querySelectorAll(".parallax");

                elements.forEach(el => {
                    const speed = parseFloat(el.getAttribute("data-speed"));
                    el.style.transform = `translateY(${scrollY * speed}px)`;
                    // console.log(`${el.id} has style transform as ${el.style.transform} with data-speed of ${el.getAttribute("data-speed")}`);
                });

                ticking = false;
            })
            ticking = true;
        }

        //parallaxAttached = true;

    }

}