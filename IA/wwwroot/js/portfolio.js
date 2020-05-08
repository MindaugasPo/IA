$(document).ready(function () {
    $(document).on("click", "#historic-transactions", function (event) {
        event.preventDefault();
        event.stopPropagation();

        $(".historic-portfolio-transactions").toggleClass("d-none");
    });
});
