mie = window.roomie || {};
window.roomie.ui = window.roomie.ui || {};
window.roomie.ui.softNavigate = window.roomie.ui.softNavigate || {};

(function(namespace) {
    if (namespace.loaded) {
        return;
    }
    namespace.loaded = true;

    var animatinoSpeed = 250;
    
    var navigate = namespace.navigate = function (path, pushState, callback) {
        roomie.ui.notifications.add('loading...');

        var actionCount = 3;

        var page;

        var done = function() {
            actionCount--;
            
            if (actionCount == 0) {
                replace(page, callback);
            }
        };

        $('#title, #content').animate({
            'opacity': 0
        }, animatinoSpeed, null, done);
        
        if (pushState) {
            history.pushState({ path: path }, path, path);
        }
        
        $.get(path)
            .success(function(data) {
                page = data;
                done();
            })
            .fail(function() {
                window.location.href = path;
            });
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

    var replace = namespace.replace = function (page, callback) {
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

        var actionCount = 2;
        
        var done = function () {
            actionCount--;

            if (actionCount == 0) {
                $('#title, #content').css('opacity', '');
                sizeGhostHeader();
                sizeGhostFooter();
                
                if (callback) {
                    callback();
                }
            }
        };

        $('#title, #content').animate({
            'opacity': 1
        }, animatinoSpeed, null, done);
    };

    var event = function (e) {
        var path = $(this).attr('href');

        if (path.indexOf('/') == 0) {
            e.preventDefault();
            roomie.ui.slideMenu.hide();
            navigate(path, true);
        }
    };

    $('#page').delegate('a', 'click', event);

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