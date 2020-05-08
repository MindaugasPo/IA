$(document).ready(function () {
    $(document).on("click", "#submit-new-asset-form", function (event) {
        event.preventDefault();
        event.stopPropagation();

        Ajax("POST",
            "/Asset/Create",
            $("#new-asset-form").serialize(),
            function (result) {
                if (result) {
                    if (result.success) {
                        triggerMainMenu("#main-menu-assets");
                    } else {
                        $("#new-asset-form .validation-error").removeClass("d-none");
                        $("#new-asset-form .validation-error").text(result.message);
                    }
                } else {
                    $("#new-asset-form .validation-error").text("Something went wrong");
                }
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