$(document).ready(function () {
    $("#main-menu-assets").on("click", function (event) {
        event.preventDefault();
        event.stopPropagation();

        Ajax(
            "GET",
            "/Asset/GetAll",
            {},
            fillMain
        );
    });

    $("#main-menu-transactions").on("click", function (event) {
        event.preventDefault();
        event.stopPropagation();

        Ajax(
            "GET",
            "/Transaction/GetAll",
            {},
            fillMain
        );
    });

    $("#main-menu-portfolio").on("click", function (event) {
        event.preventDefault();
        event.stopPropagation();

        renderAllPortfolios();
    });
});