function debug(message) {
    return;
    $('#navigation').append('<div class="debugMessage">' + message + '</div>');
}

$(window).resize(function () {
    
});

$(document).ready(function () {

    /*
    $('.button').mouseup(function () {
    debug('click!');
    setTimeout('updateDevices(null, null);', 500);
    });*/


});


var botZoom = function () {
    $(this).unbind();
    var zoom = $(this).css('zoom');
    zoom = (zoom == 1) ? 4 : 1;
    $(this).animate({ zoom: zoom }, 1000, function() {
        $(this).click(botZoom);
    });
};

$('.roomieBot').click(botZoom);