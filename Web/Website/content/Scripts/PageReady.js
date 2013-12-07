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



var botZoom = function () {
    $(this).unbind();
    roomie.ui.notifications.add('clicking my faaace!', 1000);
    var zoom = $(this).css('zoom');
    zoom = (zoom == 1) ? 4 : 1;
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
