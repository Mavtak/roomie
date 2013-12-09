var roomie = roomie || {};
roomie.ui = roomie.ui || {};
roomie.ui.slideMenu = roomie.ui.slideMenu || {};

(function (namespace) {
    if (namespace.loaded) {
        return;
    }
    namespace.loaded = true;
    
    var $page = namespace.$page || $('#page');
    var $menu = namespace.$menu || $('#slideOutMenu');
    var $menuItems = namespace.$menuItems || function () { return $menu.find('.item'); };
    var $header = namespace.$page || $('#headerRow');
    var $content = namespace.$content || $('#content');
    var $footer = namespace.$page || $('#footerRow');
    var $button = namespace.$window || $('#menuButton');
    var $window = namespace.$window || $(window);
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
            },
            content: {
                left: width,
            }
        };

        return result;
    };

    var setSlideOutStyles = namespace.setSlideOutStyles = function() {
        var metrics = getSlideOutMetrics();
        
        $menu.css('top', metrics.menu.top);
        $menu.css('height', metrics.menu.height);
        $menu.css('width', metrics.menu.width);
        
        $content.css('left', metrics.content.left);
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
        
        $content.css('position', 'relative');
        $menu.css('position', 'fixed');
        $menu.css('display', 'inline-block');
        $menu.css('overflow-y', 'auto');

        var metrics = getSlideOutMetrics();
        
        $menu.css('top', metrics.menu.top);
        $menu.css('height', metrics.menu.height);

        var animationCount = 2;

        var done = function() {
            animationCount--;
            
            if (animationCount == 0) {
                animating = false;
                
                if (callback) {
                    callback();
                }
            }
        };
        
        $menu.animate({
            'width': metrics.menu.width
        }, animationSpeed, null, done);

        $content.animate({
            'left': metrics.content.left
        }, animationSpeed, null, done);
    };

    var hide = namespace.hide = function (callback) {
        if (animating) {
            return;
        }
        
        visible = false;
        unbindResize();
        $overlay.remove();
        
        var animationCount = 2;

        var done = function () {
            animationCount--;
            
            $content.css('left', '');
            $menu.css('display', '');

            if (animationCount == 0) {
                animating = false;
                
                if (callback) {
                    callback();
                }
            }
        };
        
        $menu.animate({
            'width': 0
        }, animationSpeed, null, done);

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
})(roomie.ui.slideMenu);
