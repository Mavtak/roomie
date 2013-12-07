mie = window.roomie || {};
window.roomie.ui = window.roomie.ui || {};
window.roomie.ui.softNavigate = window.roomie.ui.softNavigate || {};

(function(namespace) {

    var navigate = namespace.navigate = function (path, pushState) {
        roomie.ui.notifications.add('loading...');

        $('#title').css('visibility', 'hidden');
        $('#content').css('visibility', 'hidden');

        if (pushState) {
            history.pushState({ path: path }, path, path);
        }
        
        $.get(path)
            .success(replace)
            .fail(failure);
    };

    var replace = namespace.replace = function (page) {
        var $page = $(page);

        var replaceContent = function (source, destination) {
            destination = destination || source;
            var $new = $(source, $page);
            var $existing = $(destination);

            $existing.html($new.html());
        };

        console.log($('#title', $page).html());

        replaceContent('#title', 'title');
        replaceContent('#title');
        replaceContent('#content');
        replaceContent('#slideOutMenu');
        
        $('#title').css('visibility', '');
        $('#content').css('visibility', '');
        sizeGhostHeader();
        sizeGhostFooter();
    };

    var failure = namespace.fail = function() {
        roomie.ui.notifications.add('could not load page.');
    };

    var first = true;
    window.onpopstate = function () {
        
        if (first) {
            first = false;
            
            return;
        }

        var path = history.state.path;
        navigate(path, false);
    };

})(window.roomie.ui.softNavigate);