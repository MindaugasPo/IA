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