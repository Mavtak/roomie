
navigationVisible = true;

function debug(message) {
    return;
    $('#navigation').append('<div class="debugMessage">' + message + '</div>');
}

function fixLinks() {
    //http://stackoverflow.com/questions/179713/how-to-change-the-href-for-a-hyperlink-using-jquery
    // curse you, GoDaddy!
    $("a").each(function () {
       this.href = this.href.replace(/roomiebot.com\/root\//, '');
    });
}

function fixUrl() {
    if (location.href.indexOf('/roomiebot.com/root') != -1) {
        location.href = location.href.replace(/\/roomiebot.com\/root/, '');
    }
}

$(window).resize(function () {
    //setTimeout('adjustLayout()', 100);
    adjustLayout();
});

$(document).ready(function () {
    fixLinks();
    fixUrl();

    /*
    $('.button').mouseup(function () {
    debug('click!');
    setTimeout('updateDevices(null, null);', 500);
    });*/

});