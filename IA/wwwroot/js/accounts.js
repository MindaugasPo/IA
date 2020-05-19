$(document).ready(function () {
    $(document).on("click", "#account-signout", function (event) {
        event.preventDefault();
        event.stopPropagation();

        Ajax(
            "POST",
            "/Account/SignOut",
            {},
            function (result) {
                if (result && result.success) {
                    window.location = "/";
                }
            }
        );
    });

    $(document).on("click", "#signin-submit", function (event) {
        event.preventDefault();
        event.stopPropagation();

        Ajax(
            "POST",
            "/Account/Signin",
            $("#account-signin-form").serialize(),
            function (result) {
                if (result && result.success) {
                        window.location = "/";
                } else {
                    $("#signin-failure-container").removeClass("d-none");
                }
            }
        );
    });

    $(document).on("click", "#account-signin", function (event) {
        event.preventDefault();
        event.stopPropagation();

        Ajax(
            "GET",
            "/Account/SignIn",
            {},
            fillMain
        );
    });
    $(document).on("click", "#account-register", function (event) {
        event.preventDefault();
        event.stopPropagation();

        Ajax(
            "GET",
            "/Account/Register",
            {},
            fillMain
        );
    });

    $(document).on("click", "#register-submit", function (event) {
        event.preventDefault();
        event.stopPropagation();

        Ajax(
            "POST",
            "/Account/Register",
            $("#register-form").serialize(),
            function(result) {
                if (result) {
                    if (result.success) {
                        $("#register-form-container").addClass("d-none");
                        $("#register-success-container").removeClass("d-none");
                    } else {
                        $("#registration-errors").html(result.message);
                        $("#register-failure-container").removeClass("d-none");
                    }
                } else {
                    $("#register-failure-container").removeClass("d-none");
                }
            }
        );
    });
});