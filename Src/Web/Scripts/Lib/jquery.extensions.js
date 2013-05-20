jQuery.extend({
    postJson: function(url, data, callback, errorCallback, completedCallback) {
        // shift arguments if data argument was omited
        if (jQuery.isFunction(data)) {
            type = type || callback;
            callback = data;
            data = {};
        }

        return jQuery.ajax({
            type: "POST",
            contentType: "application/json; charset=UTF-8",
            url: url,
            data: data,
            success: callback,
            error: errorCallback,
            completed: completedCallback,
            dataType: "json"
        });
    },
});
