(function() {
    var namespace = createNamespace('roomie.ui.pages.devices.index');
    if (namespace.loaded) {
        return;
    }
    namespace.loaded = true;

    var cancelData = {};

    namespace.init = function () {
        roomie.ui.softNavigate.exitPage = exit;

        setupCollapseHeaders();
        updateDevicesContinuously();
    };

    var exit = namespace.exit = function () {
        if (cancelData.timeout) {
            clearTimeout(cancelData.timeout);
            delete cancelData.timeout;
        }
        
        if (cancelData.request) {
            cancelData.request.abort();
            delete cancelData.request;
        }
    };

    var setupCollapseHeaders = namespace.setupCollapseHeaders = function() {
        $('.collapse-next').click(function(e) {
            e.preventDefault();
            var $this = $(this);
            $this.next().toggle();
        });
    };

    var replaceDivs = namespace.replaceDivs = function(data) {
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
    };


    var updateDevicesContinuously = namespace.updateDevicesContinuously = function() {
        var success = function() {
            cancelData.timeout = setTimeout(updateDevicesContinuously, 1000);
        };

        var failure = function() {
            cancelData.timeout = setTimeout(updateDevicesContinuously, 5000);
        };

        updateDevices(success, failure);
    };

    var updateDevices = namespace.updateDevicesContinuously = function(success, failure) {
        debug('updating...');
        cancelData.request = $.ajax({
            url: "/Device/IndexAjax",
            dataType: "json",
            type: 'get',
            success: function(data) {
                replaceDivs(data);
                debug('...success');
                if (success != null) {
                    success();
                }
            },
            error: function() {
                debug('...failure');
                if (failure != null) {
                    failure();
                }
            }
        });
    };
})();