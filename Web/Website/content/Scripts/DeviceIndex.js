(function() {
    var namespace = createNamespace('roomie.ui.pages.devices.index');
    if (namespace.loaded) {
        return;
    }
    namespace.loaded = true;

    namespace.init = function () {
        setupCollapseHeaders();
        updateDevicesContinuously();
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
            setTimeout(updateDevicesContinuously, 1000);
        };

        var failure = function() {
            setTimeout(updateDevicesContinuously, 5000);
        };

        updateDevices(success, failure);
    };

    var updateDevices = namespace.updateDevicesContinuously = function(success, failure) {
        debug('updating...');
        $.ajax({
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