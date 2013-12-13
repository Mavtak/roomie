function debug(message) {
    return;
    $('#navigation').append('<div class="debugMessage">' + message + '</div>');
}

$(window).resize(function () {
    sizeGhostHeader();
    sizeGhostFooter();
});

$(document).ready(function () {

    /*
    $('.button').mouseup(function () {
    debug('click!');
    setTimeout('updateDevices(null, null);', 500);
    });*/


});

$('#noJsMessage').css('display', 'none');
$('#page').css('display', '');

roomie.ui.navigationMenu = new roomie.ui.SlideMenu($('#navigationMenu'), $('#navigationMenuToggle'), 'left');
roomie.ui.pageMenu = new roomie.ui.SlideMenu($('#pageMenu'), $('#pageMenuToggle'), 'right');

var botZoom = function () {
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

var sizeGhostHeader = function() {
    $ghostHeaderRow.css('height', $headerRow.height());
};

detatchHeader();
sizeGhostHeader();

var $footerRow = $('#footerRow');
var $ghostFooterRow;

var detatchFooter = function () {
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

var sizeGhostFooter = function () {
    $ghostFooterRow.css('height', $footerRow.height());
};

detatchFooter();
sizeGhostFooter();
