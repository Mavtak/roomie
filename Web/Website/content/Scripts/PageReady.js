
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

function showNavigation() {
    navigationVisible = true;
    $('.sidebarColumn').css('display', '');
    $('#navigationSlice').css('display', 'none');
    $('#content').css('border-top', 'none');

    adjustLayout();
}

function hideNavigation() {
    navigationVisible = false;
    $('.sidebarColumn').css('display', 'none');
    $('#navigationSlice').css('display', '');
    $('#content').css('border-top', '');

    adjustLayout();
}

function toggleNavigation() {
    if (navigationVisible) {
        hideNavigation();
    }
    else {
        showNavigation();
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

    hideNavigation();
    $('#colorStrip').click(function () { toggleNavigation(); });
    /*
    $('.button').mouseup(function () {
    debug('click!');
    setTimeout('updateDevices(null, null);', 500);
    });*/

});