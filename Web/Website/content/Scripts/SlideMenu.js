var roomie = roomie || {};
roomie.ui = roomie.ui || {};
roomie.ui.slideMenu = roomie.ui.slideMenu || {};

(function(namespace) {
    var $page = namespace.$page || $('#page');
    var $menu = namespace.$menu || $('#slideOutMenu');
    var $content = namespace.$content || $('#content');
    var $button = namespace.$window || $('#menuButton');
    var $window = namespace.$window || $(window);
    var $overlay = $('<div />');

    $overlay.css('position', 'absolute');
    $overlay.css('left', 0);
    $overlay.css('right', 0);
    $overlay.css('top', 0);
    $overlay.css('bottom', 0);
    $overlay.css('z-inde', '50');

    var visible = false;
    
    var getSlideOutMetrics = namespace.getSlideOutMetrics = function () {
        var result = {
            width: Math.max(Math.min($page.width(), 250), $content.width() * .75) + 'px',
            height: $content.height()
        };

        return result;
    };

    var setSlideOutStyles = namespace.setSlideOutStyles = function() {
        var metrics = getSlideOutMetrics();
        $content.css('left', metrics.width);
        $menu.css('width', metrics.width);
        $menu.css('height', metrics.height);
    };

    var bindResize = namespace.bindResize = function() {
        $window.resize(setSlideOutStyles);
    };

    var unbindResize = namespace.unbindResize = function() {
        $window.unbind("resize", setSlideOutStyles);
    };

    var show = namespace.show = function () {
        visible = true;
        bindResize();
        $content.append($overlay);
        $overlay.click(hide);

        var metrics = getSlideOutMetrics();
        
        $content.css('position', 'relative');
        $menu.css('width', 0);
        $menu.css('height', metrics.height);
        $menu.css('display', 'inline-block');

        setSlideOutStyles();
    };

    var hide = namespace.hide = function () {
        visible = false;
        unbindResize();
        $overlay.remove();
        
        $content.css('left', '');
        $menu.css('display', '');
    };

    var toggle = namespace.toggle = function() {
        if (visible) {
            hide();
        } else {
            show();
        }
    };

    $button.click(function() {
        toggle();
    });
})(roomie.ui.slideMenu);
