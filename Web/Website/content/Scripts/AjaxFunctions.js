function replaceDivs(data) {
    for (x in data['replacements']) {
        id = '#' + data['replacements'][x]['id'];
        $(id).replaceWith(data['replacements'][x]['html']);
    }
}


function updateDevicesContinuously() {
    updateDevices(function ()
    {
        setTimeout('updateDevicesContinuously()', 5000);
    },
    function ()
    {
        setTimeout('updateDevicesContinuously()', 10000);
    }
    );
}

function updateDevices(success, failure) {
    debug('updating...');
    $.ajax({
        url: "/Device/IndexAjax",
        dataType: "json",
        type: 'post',
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