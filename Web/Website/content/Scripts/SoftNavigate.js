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

    var stashScriptTags = function (html) {
        html = html || '';
        var result = html.replace(/<script([\s\S]*?)<\/script>/gi, '<hack$1</hack>');

        return result;
    };

    var recoverScriptTags = function(html) {
        html = html || '';
        var result = html.replace(/<hack([\s\S]*?)<\/hack>/gi, '<script$1</script>');

        return result;
    };

    var replace = namespace.replace = function (page) {
        page = '<div>' + page + '</div>';
        page = stashScriptTags(page);

        var $page = $(page);

        var replaceContent = function (source, destination) {
            destination = destination || source;
            var $new = $(source, $page);
            var $existing = $(destination);

            $existing.html(recoverScriptTags($new.html()));
        };

        replaceContent('title');
        replaceContent('#title');
        replaceContent('#content');
        replaceContent('#slideOutMenu');
        replaceContent('#pageSpecificScripts');

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