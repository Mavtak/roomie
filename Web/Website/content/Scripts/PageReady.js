(function () {
    var namespace = createNamespace('roomie.ui');
    if (namespace.startup) {
        return;
    }
    namespace.startup = true;

    var roomie = window.roomie;
    
   function debug(message) {
        return;
        $('#navigation').append('<div class="debugMessage">' + message + '</div>');
    }

    $(window).resize(function() {
        sizeGhostHeader();
        sizeGhostFooter();
    });

    $(document).ready(function() {

        /*
        $('.button').mouseup(function () {
        debug('click!');
        setTimeout('updateDevices(null, null);', 500);
        });*/


    });

    roomie.ui.navigationMenu = new namespace.SlideMenu($('#navigationMenu'), $('#navigationMenuToggle'), 'left');
    roomie.ui.pageMenu = new namespace.SlideMenu($('#pageMenu'), $('#pageMenuToggle'), 'right');
    roomie.ui.pageMenu.hideButtonForEmptyMenu();

    var drag = function(direction) {
        var showThis;
        var hideThis;

        if (direction == 'left') {
            showThis = roomie.ui.pageMenu;
            hideThis = roomie.ui.navigationMenu;
        } else if (direction == 'right') {
            showThis = roomie.ui.navigationMenu;
            hideThis = roomie.ui.pageMenu;
        } else {
            return;
        }

        if (showThis.visible) {
            return;
        }

        if (hideThis.visible) {
            hideThis.hide();
            return;
        }

        showThis.show();
    };

    var dragContent = function () {
        var $page = $('#page');
        var startTouch;

        $page[0].addEventListener('touchstart', function (e) {
            startTouch = e.touches[0];
        });

        $page[0].addEventListener('touchend', function (e) {
            var endTouch = e.changedTouches[e.changedTouches.length - 1];

            var verticalDelta = endTouch.screenY - startTouch.screenY;
            var horizontalDelta = endTouch.screenX - startTouch.screenX;

            if (Math.abs(horizontalDelta) < 25 || Math.abs(horizontalDelta)  < Math.abs(verticalDelta)) {
                return;
            }

            drag((horizontalDelta < 0) ? 'left' : 'right');
        });
    };
    
    dragContent();
    
    var botZoom = function() {
        $(this).unbind();

        var zoom = $(this).css('zoom');
        if (zoom == 1) {
            zoom = 2;
            roomie.ui.notifications.add('you\'re clicking my faaace!', 1000);
        } else {
            zoom = 1;
            roomie.ui.notifications.add('it\s nice to feel normal again.', 1000);
        }

        $(this).animate({ zoom: zoom }, 1000, function() {
            $(this).click(botZoom);
        });
    };

    $('.roomieBot').click(botZoom);

    var $page = $('#page');
    var $headerRow = $('#headerRow');
    var $ghostHeaderRow;

    var detatchHeader = function() {
        $headerRow.css('position', 'fixed');
        $headerRow.css('z-index', '50');
        $headerRow.css('top', '0');
        $headerRow.css('left', '0');
        $headerRow.css('right', '0');
        $headerRow.css('background-color', $page.css('background-color'));

        if (!$ghostHeaderRow) {
            $ghostHeaderRow = $('<div />');
            $page.prepend($ghostHeaderRow);
        }
    };

    var sizeGhostHeader = namespace.sizeGhostHeader = function() {
        $ghostHeaderRow.css('height', $headerRow.height());
    };

    detatchHeader();
    sizeGhostHeader();

    var $footerRow = $('#footerRow');
    var $ghostFooterRow;

    var detatchFooter = namespace.sizeGhostFooter = function() {
        $footerRow.css('position', 'fixed');
        $footerRow.css('z-index', '49');
        $footerRow.css('bottom', '0');
        $footerRow.css('left', '0');
        $footerRow.css('right', '0');
        $footerRow.css('background-color', $page.css('background-color'));

        if (!$ghostFooterRow) {
            $ghostFooterRow = $('<div />');
            $page.append($ghostFooterRow);
        }
    };

    var sizeGhostFooter = function() {
        $ghostFooterRow.css('height', $footerRow.height());
    };

    detatchFooter();
    sizeGhostFooter();

})();

(function () {
    var namespace = createNamespace('roomie.ui');
    if (namespace.$contentSpringShade) {
        return;
    }

    var $shade = namespace.$contentSpringShade = $('<div />');
    $shade.insertAfter($('#content'));
    $shade.css('background-color', $('#page').css('background-color'));
    $shade.css('position', 'relative');
    $shade.css('pointer-events', 'none');

    var sizeShade = namespace.sizeShade = function () {
        $shade.height(0);
        $shade.height($('#page').height() - $('#content').height() - $('#content').position().top - $('#footer').height() - 16);
    };

    $(window).resize(sizeShade);
    sizeShade();
})();
