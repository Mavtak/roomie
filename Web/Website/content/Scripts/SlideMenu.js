(function () {
    var namespace = createNamespace('roomie.ui.slideMenu');
    if (namespace.loaded) {
        return;
    }
    namespace.loaded = true;
    

    var $page = $('#page');
    var $menu = $('#navigationMenu');
    var $menuItems = function () { return $menu.find('.item .content'); };
    var $header = $('#headerRow');
    var $content = $('#content');
    var $footer = $('#footerRow');
    var $button = $('#navigationMenuToggle');
    var $window =  $(window);
    var $overlay = $('<div />');

    var animationSpeed = 250;
    
    $overlay.css('position', 'absolute');
    $overlay.css('left', 0);
    $overlay.css('right', 0);
    $overlay.css('top', 0);
    $overlay.css('bottom', 0);
    $overlay.css('z-inde', '50');

    var visible = false;

    var animating = false;

    var maxWidth = namespace.maxWidth = function($elements) {
        var result = 0;

        $elements.each(function() {
            var width = $(this).outerWidth(true);
            
            if (width > result) {
                result = width;
            }
        });

        return result;
    };
    
    var getSlideOutMetrics = namespace.getSlideOutMetrics = function () {

        var width = Math.min($page.width(), maxWidth($menuItems()) + 25);

        var result = {
            menu: {
                width: width,
                top: $header.height(),
                height: $footer.offset().top - $header.height()
            }
        };

        return result;
    };

    var setSlideOutStyles = namespace.setSlideOutStyles = function(metrics, justMenu) {
        if (!metrics.menu) {
            metrics = getSlideOutMetrics();
        }

        $menu.css('top', metrics.menu.top);
        $menu.css('height', metrics.menu.height);
        $menu.css('width', metrics.menu.width);
        
        if (justMenu) {
            return;
        }
        
        $content.css('left', metrics.menu.width);
    };

    var bindResize = namespace.bindResize = function() {
        $window.resize(setSlideOutStyles);
    };

    var unbindResize = namespace.unbindResize = function() {
        $window.unbind("resize", setSlideOutStyles);
    };

    var show = namespace.show = function (callback) {
        if (animating) {
            return;
        }

        animating = true;

        visible = true;
        bindResize();
        $content.append($overlay);
        $overlay.click(hide);

        var metrics = getSlideOutMetrics();

        setSlideOutStyles(metrics, true);

        var done = function() {
            animating = false;
                
            if (typeof (callback) == 'function') {
                callback();
            }
        };
        
        $content.animate({
            'left': metrics.menu.width
        }, animationSpeed, null, done);
    };

    var hide = namespace.hide = function (callback) {
        if (animating) {
            return;
        }
        
        visible = false;
        unbindResize();
        $overlay.remove();
        
        var done = function () {
            $content.css('left', '');
            $menu.css('width', '');
            animating = false;
            
            if (typeof (callback) == 'function') {
                callback();
            }
        };
        $content.animate({
            'left': 0
        }, animationSpeed, null, done);
    };

    var toggle = namespace.toggle = function(callback) {
        if (visible) {
            hide(callback);
        } else {
            show(callback);
        }
    };

    $button.click(function() {
        toggle();
    });
})();
