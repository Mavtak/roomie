(function() {
    var namespace = createNamespace('roomie.ui.pages.devices.index');
    if (namespace.loaded) {
        return;
    }
    namespace.loaded = true;

    var $content = $('#content');
    var $pageMenu = $('#pageMenu');
    var $header = $('#header');
    
    var cancelData = {};

    namespace.init = function () {
        roomie.ui.softNavigate.exitPage = exit;

        setupCollapseHeaders();
        setupPageMenuItems();
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

    var getHeaderByLocation = namespace.getHeaderByLocation = function (location) {
        var $matches = $();
        var $misses = $();

        //TODO: improve
        $content.find('.collapse-next').each(function() {
            var $this = $(this);
            var location2 = $this.attr('data-location');
            if (location.indexOf(location2) == 0) {
                $matches.push($this);
            } else {
                $misses.push($this);
            }
        });

        var result = {
            $matches: $matches,
            $misses: $misses
        };

        return result;
    };

    var showLocation = namespace.showLocation = function(location) {
        var headers = getHeaderByLocation(location);

        headers.$matches.each(function() {
            var $this = $(this);

            $this.next().show();
        });

        headers.$misses.each(function() {
            var $this = $(this);

            $this.next().hide();
        });

        var scroll = function() {
            var $exactLocationHeader = headers.$matches[headers.$matches.length - 1];
            var scrollPosition = $exactLocationHeader.offset().top - $header.height();

            $('body').animate({
                scrollTop: scrollPosition
            }, 250);
        };
        
        roomie.ui.pageMenu.hide(scroll);
    };

    var setupCollapseHeaders = namespace.setupCollapseHeaders = function() {
        $('.collapse-next').click(function(e) {
            e.preventDefault();
            var $this = $(this);

            $this.next().toggle();
        });
    };

    var setupPageMenuItems = namespace.setupPageMenuItems = function() {
        $pageMenu.find('.item').click(function(e) {
            e.preventDefault();
            var $this = $(this);

            var location = $this.attr('data-location');
            showLocation(location);
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
        cancelData.request = $.ajax({
            url: "/Device/IndexAjax",
            dataType: "json",
            type: 'get',
            success: function(data) {
                replaceDivs(data);
                if (success != null) {
                    success();
                }
            },
            error: function() {
                if (failure != null) {
                    failure();
                }
            }
        });
    };
})();