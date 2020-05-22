$(document).ready(function () {
    $(document).on("click", "#historic-transactions", function (event) {
        event.preventDefault();
        event.stopPropagation();

        $("#historic-portfolio-transactions-container").toggleClass("d-none");
    });

    $(document).on("click", ".select-portfolio", function (event) {
        Ajax(
            "GET",
            "/Portfolio/Get",
            { "id": $(this).data("portfolio-id") },
            function (result) {
                if (result) {
                    $("#portfolio-data-container").html(result);
                }
            }
        );
    });

    $(document).on("click", "#submit-portfolio-create-form", function (event) {
        event.preventDefault();
        event.stopPropagation();
        var portfolioId = $("#portfolio-form #portfolio-id").val();
        Ajax(
            "POST",
            "/Portfolio/Create",
            $("#portfolio-form").serialize(),
            function (result) {
                if (result) {
                    if (result.success) {
                        renderAllPortfolios(portfolioId);
                    } else {
                        $("#portfolio-form .validation-error").removeClass("d-none");
                        $("#portfolio-form .validation-error").text(result.message);
                    }
                } else {
                    $("#portfolio-form .validation-error").text("Something went wrong");
                }
            }
        );
    });

    $(document).on("click", "#create-portfolio", function (event) {
        event.preventDefault();
        event.stopPropagation();
        Ajax(
            "GET",
            "/Portfolio/Create",
            {},
            fillMain
        );
    });
});

function renderAllPortfolios(selectedPortfolio) {
    var data = {};
    if (selectedPortfolio) {
        data = { "selectedPortfolioId": selectedPortfolio };
    }
    Ajax(
        "GET",
        "/Portfolio/GetAll",
        data,
        fillMain
    );
}