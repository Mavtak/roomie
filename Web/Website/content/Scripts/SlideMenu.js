(function () {
    var namespace = createNamespace('roomie.ui');
    if (namespace.SlideMenu) {
        return;
    }
    
    var SlideMenu = namespace.SlideMenu = function ($menu, $button, side) {
        var self = this;

        this.$menu = $menu;
        this.$button = $button;
        this.side = side;
        
        this.$page = $('#page');
        this.$menuItems = function () { return this.$menu.find('.item .content'); };
        this.$header = $('#header');
        this.$content = $('#content');
        this.$footer = $('#footer');
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
                height: this.$footer.offset().top - this.$header.offset().top - this.$header.height()
            }
        };
        
        if (this.side == 'right') {
            result.menu.left = this.$header.offset().left + this.$header.width() - result.menu.width;
        }

        return result;
    };

    SlideMenu.prototype.setSlideOutStyles = function(metrics, justMenu) {
        if (!metrics) {
            metrics = this.getSlideOutMetrics();
        }

        this.$menu.css('top', metrics.menu.top);
        this.$menu.css('height', metrics.menu.height);
        this.$menu.css('width', metrics.menu.width);
        this.$menu.css('left', metrics.menu.left);
        
        if (justMenu) {
            return;
        }
        
        this.$content.css(this.side, metrics.menu.width);
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

        var animation = {};
        animation[this.side] = metrics.menu.width;
        this.$content.animate(animation, animationSpeed, null, done);
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
            self.$content.css(self.side, '');
            self.$menu.css('width', '');
            self.animating = false;
            
            if (typeof (callback) == 'function') {
                callback();
            }
        };
        
        var animation = {};
        animation[this.side] = 0;
        this.$content.animate(animation, animationSpeed, null, done);
    };

    SlideMenu.prototype.toggle = function(callback) {
        if (this.visible) {
            this.hide(callback);
        } else {
            this.show(callback);
        }
    };
    
    SlideMenu.prototype.hideButtonForEmptyMenu = function() {
        var hasItems = this.$menu.children().length > 0;
        
        if (hasItems) {
            this.$button.show();
        } else {
            this.$button.hide();
        }
    }
})();
