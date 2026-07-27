window.nav = {
    registerOutsideClick: function (dotnetObj) {
        document.addEventListener("click", function (e) {
            const nav = document.querySelector(".navbar");
            const toggle = document.querySelector(".nav-toggle");

            if (!nav.contains(e.target) && e.target !== toggle) {
                dotnetObj.invokeMethodAsync("CloseNav");
            }
        })
    }
}