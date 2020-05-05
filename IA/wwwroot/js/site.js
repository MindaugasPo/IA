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