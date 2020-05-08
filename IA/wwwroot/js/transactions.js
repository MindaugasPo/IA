$(document).ready(function () {
    $(document).on("click", "#submit-new-transaction-form", function (event) {
        event.preventDefault();
        event.stopPropagation();

        Ajax("POST",
            "/Transaction/Create",
            $("#new-transaction-form").serialize(),
            function (result) {
                if (result) {
                    if (result.success) {
                        triggerMainMenu("#main-menu-portfolio");
                    } else {
                        $("#new-transaction-form .validation-error").removeClass("d-none");
                        $("#new-transaction-form .validation-error").text(result.message);
                    }
                } else {
                    $("#new-transaction-form .validation-error").text("Something went wrong");
                }
            }
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
});
