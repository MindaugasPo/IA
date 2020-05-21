$(document).ready(function () {
    $(document).on("click", "#submit-transaction-update-form", function (event) {
        event.preventDefault();
        event.stopPropagation();
        submitTransaction("/Transaction/Update");
    });
    $(document).on("click", "#submit-transaction-create-form", function (event) {
        event.preventDefault();
        event.stopPropagation();
        submitTransaction("/Transaction/Create");
    });
    function submitTransaction(url) {
        var portfolioId = $("#transaction-form #PortfolioId").val();
        Ajax("POST",
            url,
            $("#transaction-form").serialize(),
            function (result) {
                if (result) {
                    if (result.success) {
                        renderAllPortfolios(portfolioId);
                    } else {
                        $("#transaction-form .validation-error").removeClass("d-none");
                        $("#transaction-form .validation-error").text(result.message);
                    }
                } else {
                    $("#transaction-form .validation-error").text("Something went wrong");
                }
            }
        );
    }

    $(document).on("click", "#new-position", function (event) {
        event.preventDefault();
        event.stopPropagation();

        Ajax(
            "GET",
            "/Transaction/Create",
            { "PortfolioId": $("#portfolio-id").val() },
            fillMain
        );
    });

    $(document).on("click", ".delete-position", function (event) {
        event.preventDefault();
        event.stopPropagation();

        var transactionId = $(this).data("transaction-id");

        Ajax(
            "POST",
            "/Transaction/Delete",
            { "id": transactionId },
            function (result) {
                if (result) {
                    $("#tr-" + transactionId).remove();
                } else {
                    fillMain("Delete transaction failed");
                }
            }
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

    $(document).on("click", ".edit-position", function (event) {
        event.preventDefault();
        event.stopPropagation();

        var transactionId = $(this).data("transaction-id");

        Ajax(
            "GET",
            "/Transaction/Edit",
            { "id": transactionId },
            fillMain
        );
    });
});
