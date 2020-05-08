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

function fillMain(result) {
    if (result) {
        $("main").html(result);
    }
}

function triggerMainMenu(item) {
    $(item).trigger("click");
}

$("nav a").on("click", function(event) {
    $(".nav-link").removeClass("active");
    $(this).addClass("active");
});