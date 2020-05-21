$(document).ready(function () {
    $(document).on("click", "#historic-transactions", function (event) {
        event.preventDefault();
        event.stopPropagation();

        $("#historic-portfolio-transactions-container").toggleClass("d-none");
    });

    $(document).on("click", ".select-portfolio", function (event) {
        renderPortfolio($(this).data("portfolio-id"));
    });

});

function renderPortfolio(id) {
    Ajax(
        "GET",
        "/Portfolio/Get",
        { "id": id },
        function (result) {
            if (result) {
                $("#portfolio-data-container").html(result);
            }
        }
    );
}