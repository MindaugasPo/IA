﻿$(document).ready(function () {
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

    $(document).on("click", "#submit-asset-price-create-form", function (event) {
        event.preventDefault();
        event.stopPropagation();

        submitAssetPrice("/Asset/CreateAssetPrice");
    });
    $(document).on("click", "#submit-asset-price-update-form", function (event) {
        event.preventDefault();
        event.stopPropagation();

        submitAssetPrice("/Asset/UpdateAssetPrice");
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

    $(document).on("click", ".asset-prices", function (event) {
        event.preventDefault();
        event.stopPropagation();
        getAssetPrices($(this).data("asset-id"));
    });

    function getAssetPrices(assetId) {
        Ajax(
            "GET",
            "/Asset/GetAssetPrices",
            { "id": assetId },
            fillMain
        );
    }

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

    $(document).on("click", ".asset-price-edit", function (event) {
        event.preventDefault();
        event.stopPropagation();

        var assetPriceId = $(this).data("asset-price-id");
        Ajax(
            "GET",
            "/Asset/EditAssetPrice",
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
            "/Asset/CreateAssetPrice",
            { "assetId": assetId },
            function (result) {
                $("#add-asset-price-container").html(result);
            }
        );
    });
});