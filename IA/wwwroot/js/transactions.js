$(document).ready(function () {
    $(document).on("click", "#submit-new-transaction-form", function (event) {
        event.preventDefault();
        event.stopPropagation();

        Ajax("POST",
            "/Transaction/Create",
            $("#new-transaction-form").serialize(),
            fillMain
        );
    });

    $(document).on("click", "#new-position", function (event) {
        event.preventDefault();
        event.stopPropagation();

        Ajax(
            "GET",
            "/Transaction/Create",
            { "PortfolioId": $("#PortfolioId").val() },
            fillMain
        );
    });

    $(document).on("click", ".close-position", function(event) {
        event.preventDefault();
        event.stopPropagation();

        var transactionId = $(this).data("transaction-id");
        var closePrice = $("#close-price-" + transactionId).val();

        Ajax(
            "POST",
            "/Transaction/Close",
            { "id": transactionId, "closePrice": closePrice },
            function (result) {
                if (result) {
                    $("#tr-" + transactionId).remove();
                } else {
                    fillMain("Closing transaction failed");
                }
            }
        );
    });
});
