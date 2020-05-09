$(document).ready(function () {
    $(document).on("click", "#submit-asset-update-form", function (event) {
        event.preventDefault();
        event.stopPropagation();

        submitAsset("/Asset/Update");
    });
    $(document).on("click", "#submit-asset-create-form", function (event) {
        event.preventDefault();
        event.stopPropagation();

        submitAsset("/Asset/Create");
    });
    function submitAsset(url) {
        Ajax("POST",
            url,
            $("#asset-form").serialize(),
            function (result) {
                if (result) {
                    if (result.success) {
                        triggerMainMenu("#main-menu-assets");
                    } else {
                        $("#asset-form .validation-error").removeClass("d-none");
                        $("#asset-form .validation-error").text(result.message);
                    }
                } else {
                    $("#asset-form .validation-error").text("Something went wrong");
                }
            }
        );
    };

    $(document).on("click", ".edit-asset", function (event) {
        event.preventDefault();
        event.stopPropagation();

        var assetId = $(this).data("asset-id");

        Ajax(
            "GET",
            "/Asset/Edit",
            { "id": assetId },
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