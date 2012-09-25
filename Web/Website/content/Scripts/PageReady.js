
navigationVisible = true;

function debug(message) {
    return;
    $('#navigation').append('<div class="debugMessage">' + message + '</div>');
}

function adjustLayout() {

    if (!navigationVisible) {
        $('.horizontalCrossSection').css('padding-right', "0px");
        $('.horizontalCrossSection').css('margin-left', "auto");
        return;
    }

    gap = $("#page").width() - 1000;
    if (gap >=125) {
        $('.horizontalCrossSection').css('padding-right', "125px");
        $('.horizontalCrossSection').css('margin-left', "auto");
    }
    else
    {
        $('.horizontalCrossSection').css('padding-right', "0px");
        $('.horizontalCrossSection').css('margin-left', "0px");
    }
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
    adjustLayout();
    fixLinks();
    fixUrl();

    /*
    $('.button').mouseup(function () {
    debug('click!');
    setTimeout('updateDevices(null, null);', 500);
    });*/

});