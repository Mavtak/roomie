mie = window.roomie || {};
window.roomie.ui = window.roomie.ui || {};
window.roomie.ui.softNavigate = window.roomie.ui.softNavigate || {};

(function(namespace) {

    var navigate = namespace.navigate = function (path) {
        roomie.ui.notifications.add('loading...');

        $('#title').css('visibility', 'hidden');
        $('#content').css('visibility', 'hidden');
        
        $.get(path)
            .success(replace)
            .fail(failure);
    };

    var replace = namespace.replace = function (page) {
        var $page = $(page);

        var replaceId = function(id) {
            var $new = $(id, $page);
            var $existing = $(id);

            $existing.html($new.html());
        };

        replaceId('#title');
        replaceId('#content');
        replaceId('#slideOutMenu');
        
        $('#title').css('visibility', '');
        $('#content').css('visibility', '');
        sizeGhostHeader();
        sizeGhostFooter();
    };

    var failure = namespace.fail = function() {
        roomie.ui.notifications.add('could not load page.');
    };

})(window.roomie.ui.softNavigate);