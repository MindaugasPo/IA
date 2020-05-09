$(document).ready(function () {
    $(document).on("click", "#submit-asset-price-create-form", function (event) {
        event.preventDefault();
        event.stopPropagation();

        submitAssetPrice("/AssetPrice/CreateAssetPrice");
    });
    $(document).on("click", "#submit-asset-price-update-form", function (event) {
        event.preventDefault();
        event.stopPropagation();

        submitAssetPrice("/AssetPrice/UpdateAssetPrice");
    });
    function submitAssetPrice(url) {
        var assetId = $("#asset-id").val();
        Ajax("POST",
            url,
            $("#asset-price-form").serialize(),
            function (result) {
                if (result) {
                    if (result.success) {
                        getAssetPrices(assetId);
                    } else {
                        $("#asset-price-form .validation-error").removeClass("d-none");
                        $("#asset-price-form .validation-error").text(result.message);
                    }
                } else {
                    $("#asset-price-form .validation-error").text("Something went wrong");
                }
            }
        );
    }
    $(document).on("click", ".asset-prices", function (event) {
        event.preventDefault();
        event.stopPropagation();
        getAssetPrices($(this).data("asset-id"));
    });

    function getAssetPrices(assetId) {
        Ajax(
            "GET",
            "/AssetPrice/GetAssetPrices",
            { "id": assetId },
            fillMain
        );
    }
    $(document).on("click", ".asset-price-edit", function (event) {
        event.preventDefault();
        event.stopPropagation();

        var assetPriceId = $(this).data("asset-price-id");
        Ajax(
            "GET",
            "/AssetPrice/EditAssetPrice",
            { "id": assetPriceId },
            function (result) {
                if (result) {
                    fillMain(result);
                }
            }
        );
    });

    $(document).on("click", "#asset-price-create", function (event) {
        event.preventDefault();
        event.stopPropagation();

        var assetId = $("#asset-id").val();
        Ajax(
            "GET",
            "/AssetPrice/CreateAssetPrice",
            { "assetId": assetId },
            function (result) {
                $("#add-asset-price-container").html(result);
            }
        );
    });

    $(document).on("click", ".asset-price-delete", function (event) {
        event.preventDefault();
        event.stopPropagation();

        var assetId = $("#asset-id").val();
        var assetPriceId = $(this).data("asset-price-id");
        Ajax(
            "POST",
            "/AssetPrice/DeleteAssetPrice",
            { "assetPriceId": assetPriceId },
            function (result) {
                getAssetPrices(assetId);
            }
        );
    });
});