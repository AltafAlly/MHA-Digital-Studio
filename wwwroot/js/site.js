document.addEventListener("DOMContentLoaded", function () {

    // =========================================================
    // NAVBAR SCROLL STATE
    // =========================================================

    const navbar = document.querySelector(".mha-navbar");

    function updateNavbar() {

        if (!navbar) {
            return;
        }

        if (window.scrollY > 40) {
            navbar.classList.add("scrolled");
        } else {
            navbar.classList.remove("scrolled");
        }

    }

    window.addEventListener("scroll", updateNavbar);

    updateNavbar();


    // =========================================================
    // SCROLL REVEAL
    // =========================================================

    const revealElements =
        document.querySelectorAll(".reveal");

    if ("IntersectionObserver" in window) {

        const revealObserver =
            new IntersectionObserver(
                function (entries, observer) {

                    entries.forEach(function (entry) {

                        if (entry.isIntersecting) {

                            entry.target.classList.add("visible");

                            observer.unobserve(entry.target);

                        }

                    });

                },
                {
                    threshold: 0.12
                }
            );


        revealElements.forEach(function (element) {

            revealObserver.observe(element);

        });

    } else {

        revealElements.forEach(function (element) {

            element.classList.add("visible");

        });

    }


    // =========================================================
    // MOBILE NAVIGATION
    // =========================================================

    const navigation =
        document.getElementById("mainNavigation");

    const navLinks =
        document.querySelectorAll(
            "#mainNavigation .nav-link"
        );


    navLinks.forEach(function (link) {

        link.addEventListener("click", function () {

            if (
                window.innerWidth < 992 &&
                navigation &&
                navigation.classList.contains("show")
            ) {

                const mobileMenu =
                    bootstrap.Collapse.getOrCreateInstance(
                        navigation
                    );

                mobileMenu.hide();

            }

        });

    });

});