var roomie = roomie || {};
roomie.ui = roomie.ui || {};
roomie.ui.slideMenu = roomie.ui.slideMenu || {};

(function(namespace) {
    var $page = namespace.$page || $('#page');
    var $menu = namespace.$menu || $('#slideOutMenu');
    var $header = namespace.$page || $('#headerRow');
    var $content = namespace.$content || $('#content');
    var $footer = namespace.$page || $('#footerRow');
    var $button = namespace.$window || $('#menuButton');
    var $window = namespace.$window || $(window);
    var $overlay = $('<div />');

    var maxWidth = namespace.maxWidth = $('.mainColumn').css('max-width').replace('px', '') * 1;

    $overlay.css('position', 'absolute');
    $overlay.css('left', 0);
    $overlay.css('right', 0);
    $overlay.css('top', 0);
    $overlay.css('bottom', 0);
    $overlay.css('z-inde', '50');

    var visible = false;
    
    var getSlideOutMetrics = namespace.getSlideOutMetrics = function () {

        var width = Math.min($page.width(), 250);

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

    var show = namespace.show = function () {
        visible = true;
        bindResize();
        $content.append($overlay);
        $overlay.click(hide);
        
        $content.css('position', 'relative');
        $menu.css('position', 'fixed');
        $menu.css('display', 'inline-block');
        $menu.css('overflow-y', 'auto');

        setSlideOutStyles();
    };

    var hide = namespace.hide = function () {
        visible = false;
        unbindResize();
        $overlay.remove();
        
        $content.css('left', '');
        $content.css('width', '');
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
