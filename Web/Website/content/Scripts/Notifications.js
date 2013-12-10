(function () {
    var namespace = createNamespace('roomie.ui.notifications');
    if (namespace.loaded) {
        return;
    }
    namespace.loaded = true;

    var $container = $('#footer');
    var $idleContent = $container.html();
    var defaultTimeout = 5000;
    var items = [];
    var cycling = false;
    var timeout = 0;

    var add = namespace.add = function ($content, timeout) {
        timeout = timeout || defaultTimeout;
        
        items.push({
            $content: $content,
            timeout: timeout
        });
        
        if (!cycling) {
            displayNext();
        }
    };

    var getNext = namespace.getNext = function() {
        var result = (items.length == 0) ? ({ $content: $idleContent }) : (items.shift());

        return result;
    };

    var displayNext = namespace.displayNext = function() {
        var next = getNext();

        setContent(next.$content);

        if (next.$content == $idleContent) {
            cycling = false;
            return;
        }

        cycling = true;

        timeout = setTimeout(displayNext, next.timeout);
    };

    var setContent = namespace.setContent = function ($content) {
        clearTimeout(timeout);
        $container.html($content);
    };

})();
