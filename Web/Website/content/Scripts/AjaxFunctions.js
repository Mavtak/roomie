function replaceDivs(data) {
    for (x in data['replacements']) {
        var id = '#' + data['replacements'][x]['id'];
        var $original = $(id);
        var originalOuterHtml = $(id).clone().wrap('<p>').parent().html();
        var replacementOuterHtml = data['replacements'][x]['html'];
        var $replacement = $(replacementOuterHtml);
        replacementOuterHtml = $replacement.clone().wrap('<p>').parent().html();

        if (originalOuterHtml != replacementOuterHtml) {
            $original.replaceWith(replacementOuterHtml);
        }
    }
}


function updateDevicesContinuously() {

    success = function ()
    {
        setTimeout('updateDevicesContinuously()', 1000);
    };

    failure = function ()
    {
        setTimeout('updateDevicesContinuously()', 5000);
    };

    updateDevices(success, failure);
}

function updateDevices(success, failure) {
    debug('updating...');
    $.ajax({
        url: "/Device/IndexAjax",
        dataType: "json",
        type: 'get',
        success: function (data) {
            replaceDivs(data);
            debug('...success');
            if (success != null) {
                success();
            }
        },
        error: function () {
            debug('...failure');
            if (failure != null) {
                failure();
            }
        }
    });
}