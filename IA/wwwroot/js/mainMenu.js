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

        Ajax(
            "GET",
            "/Portfolio/Get",
            { "id": "3DA15C4C-D24D-4881-94FE-AF666FE835EB" },
            fillMain
        );
    });
});