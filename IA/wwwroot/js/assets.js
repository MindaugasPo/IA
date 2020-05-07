$(document).ready(function () {
    $(document).on("click", "#submit-new-asset-form", function (event) {
        event.preventDefault();
        event.stopPropagation();

        Ajax("POST",
            "/Asset/Create",
            $("#new-asset-form").serialize(),
            function (result) {
                triggerMainMenu("#main-menu-assets");
            }
        );
    });

    $(document).on("click", ".asset-link", function (event) {
        event.preventDefault();
        event.stopPropagation();

        Ajax(
            "GET",
            "/Asset/GetAssetPrices",
            { "id": $(this).data("asset-id") },
            fillMain
        );
    });

    $(document).on("click", "#new-asset", function(event) {
        event.preventDefault();
        event.stopPropagation();

        Ajax(
            "GET",
            "/Asset/Create",
            {},
            fillMain
        );
    });
});