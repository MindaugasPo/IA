$(document).ready(function () {
    function fillMain(result) {
        if (result) {
            $("main").html(result);
        }
    }

    $(document).on("click", "#submit-new-transaction-form", function (event) {
        event.preventDefault();
        event.stopPropagation();

        Ajax("POST",
            "/Transaction/Create",
            $("#new-transaction-form").serialize(),
            fillMain
        );
    });

    $(document).on("click", "#new-position", function(event) {
        event.preventDefault();
        event.stopPropagation();

        Ajax(
            "GET",
            "/Transaction/Create",
            { "PortfolioId": $("#PortfolioId").val() },
            fillMain
        );
    });

    $(document).on("click", ".asset-link", function(event) {
        event.preventDefault();
        event.stopPropagation();

        Ajax(
            "GET",
            "/Asset/GetAssetPrices",
            { "id": $(this).data("asset-id") },
            fillMain
        );
    });
    $("#main-menu-assets").on("click", function(event) {
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

function Ajax(type, url, data, success) {
    var async = false;
    var cache = false;
    
    $.ajax({
        type: type,
        url: url,
        data: data,
        success: success,
        async: async,
        cache: cache
    });
}