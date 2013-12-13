(function () {
    var namespace = createNamespace('roomie.ui');
    if (namespace.SlideMenu) {
        return;
    }
    
    var SlideMenu = namespace.SlideMenu = function () {
        var self = this;
        
        this.$page = $('#page');
        this.$menu = $('#navigationMenu');
        this.$menuItems = function () { return this.$menu.find('.item .content'); };
        this.$header = $('#headerRow');
        this.$content = $('#content');
        this.$footer = $('#footerRow');
        this.$button = $('#navigationMenuToggle');
        this.$window = $(window);
        this.$overlay = $('<div />');

        this.$overlay.css('position', 'absolute');
        this.$overlay.css('left', 0);
        this.$overlay.css('right', 0);
        this.$overlay.css('top', 0);
        this.$overlay.css('bottom', 0);
        this.$overlay.css('z-inde', '50');
        
        this.animating = false;
        this.visible = false;

        this.resizeCallback = function() {
            self.setSlideOutStyles();
        };

        this.$button.click(function () {
            self.toggle();
        });
    };
    

    var animationSpeed = 250;
    
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
    
    SlideMenu.prototype.getSlideOutMetrics = function () {

        var width = Math.min(this.$page.width(), maxWidth(this.$menuItems()) + 25);

        var result = {
            menu: {
                width: width,
                top: this.$header.height(),
                height: this.$footer.offset().top - this.$header.height()
            }
        };

        return result;
    };

    SlideMenu.prototype.setSlideOutStyles = function(metrics, justMenu) {
        if (!metrics) {
            metrics = this.getSlideOutMetrics();
        }

        this.$menu.css('top', metrics.menu.top);
        this.$menu.css('height', metrics.menu.height);
        this.$menu.css('width', metrics.menu.width);
        
        if (justMenu) {
            return;
        }
        
        this.$content.css('left', metrics.menu.width);
    };

    SlideMenu.prototype.bindResize = function() {
        this.$window.resize(this.resizeCallback);
    };

    SlideMenu.prototype.unbindResize = function () {
        this.$window.unbind("resize", this.resizeCallback);
    };

    SlideMenu.prototype.show = function (callback) {
        if (this.animating) {
            return;
        }

        var self = this;
        this.animating = true;

        this.visible = true;
        this.bindResize();
        this.$content.append(this.$overlay);
        this.$overlay.click(function() {
            self.hide();
        });

        var metrics = this.getSlideOutMetrics();

        this.setSlideOutStyles(metrics, true);

        var done = function() {
            self.animating = false;
                
            if (typeof (callback) == 'function') {
                callback();
            }
        };
        
        this.$content.animate({
            'left': metrics.menu.width
        }, animationSpeed, null, done);
    };

    SlideMenu.prototype.hide = function (callback) {
        if (this.animating) {
            return;
        }

        var self = this;
        this.visible = false;
        this.animating = true;
        this.unbindResize();
        this.$overlay.remove();
        
        var done = function () {
            self.$content.css('left', '');
            self.$menu.css('width', '');
            self.animating = false;
            
            if (typeof (callback) == 'function') {
                callback();
            }
        };
        this.$content.animate({
            'left': 0
        }, animationSpeed, null, done);
    };

    SlideMenu.prototype.toggle = function(callback) {
        if (this.visible) {
            this.hide(callback);
        } else {
            this.show(callback);
        }
    };
})();
