(function () {
    var namespace = createNamespace('roomie.ui.softNavigate');
    if (namespace.loaded) {
        return;
    }
    namespace.loaded = true;

    var animatinoSpeed = 250;
    
    var navigate = namespace.navigate = function (path, pushState, preAnimation, whileNavigating, callback) {
        var page;
        var animationComplete;
        
        var done = function() {
            if (page && animationComplete) {
                finishNavigate(page, whileNavigating, callback);
            }
        };
        
        startNavigateDownload(path, pushState, function(response) {
            page = response;
            done();
        });

        var animation = function() {
            startNavigateAnimation(function () {
                animationComplete = true;
                done();
            });
        };
        
        if (preAnimation) {
            preAnimation(animation);
        } else {
            animation();
        }
    };

    var startNavigateDownload = namespace.startNavigateDownload = function (path, pushState, callback) {
        roomie.ui.notifications.setContent('loading...');
        
        if (pushState) {
            history.pushState({ path: path }, path, path);
        }
        
        $.get(path)
            .success(callback)
            .fail(function() {
                window.location.href = path;
            });
    };
    
    var startNavigateAnimation = namespace.startNavigateAnimation = function (callback) {

        var animatinoCount = 2;

        var done = function() {
            animatinoCount--;

            if (animatinoCount == 0) {
                callback();
                return;
            }
        };

        $('#header, #content').animate({
            'opacity': 0
        }, animatinoSpeed, null, done);
    };
    
    var finishNavigate = namespace.finishNavigate = function(page, whileNavigating, callback) {
        if (namespace.exitPage) {
            namespace.exitPage();
            delete namespace.exitPage;
        }
        
        roomie.ui.notifications.displayNext();
        replace(page, whileNavigating, callback);
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

    var replace = namespace.replace = function (page, whileNavigating, callback) {
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
        replaceContent('#navigationMenu');
        replaceContent('#pageMenu');
        replaceContent('#pageSpecificScripts');


        if (whileNavigating) {
            whileNavigating();
        }
        
        var actionCount = 2;
        
        var done = function () {
            actionCount--;

            if (actionCount == 0) {
                $('#header, #content').css('opacity', '');
                roomie.ui.sizeGhostHeader();
                roomie.ui.sizeGhostFooter();
                
                if (callback) {
                    callback();
                }
            }
        };

        $('#header, #content').animate({
            'opacity': 1
        }, animatinoSpeed, null, done);
    };

    var event = function (e) {
        var path = $(this).attr('href');

        if (path.indexOf('/') == 0) {
            e.preventDefault();
            navigate(path, true, function(callback) {
                roomie.ui.navigationMenu.hide(callback);
            }, function() {
                roomie.ui.pageMenu.hideButtonForEmptyMenu();
            });
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

})();
