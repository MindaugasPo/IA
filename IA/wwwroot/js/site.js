$(document).ready(function () {
    $(document).on("click", ".asset-link", function(event) {
        event.preventDefault();
        event.stopPropagation();

        Ajax(
            "GET",
            "/Asset/GetAssetPrices",
            { "id": $(this).data("asset-id") },
            function (result) {
                if (result) {
                    $("main").html(result);
                }
            });
    });
    $("#main-menu-assets").on("click", function(event) {
        event.preventDefault();
        event.stopPropagation();

        Ajax(
            "GET",
            "/Asset/GetAll",
            {},
            function(result) {
                if (result) {
                    $("main").html(result);
                }
            });
    });

    $("#main-menu-transactions").on("click", function (event) {
        event.preventDefault();
        event.stopPropagation();

        Ajax(
            "GET",
            "/Transaction/GetAll",
            {},
            function (result) {
                if (result) {
                    $("main").html(result);
                }
            });
    });

    $("#main-menu-portfolio").on("click", function (event) {
        event.preventDefault();
        event.stopPropagation();

        Ajax(
            "GET",
            "/Portfolio/Get",
            { "id": "3DA15C4C-D24D-4881-94FE-AF666FE835EB" },
            function (result) {
                if (result) {
                    $("main").html(result);
                }
            });
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