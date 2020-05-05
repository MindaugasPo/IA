$(document).ready(function () {
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
});