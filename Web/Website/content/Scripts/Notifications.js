window.roomie = window.roomie || {};
window.roomie.ui = window.roomie.ui || {};
window.roomie.ui.notifications = window.roomie.ui.notifications || {};

(function(namespace) {
    var $container = $('#footer');
    var $idleContent = $container.html();
    var timeout = 5000;
    var items = [];
    var cycling = false;

    var add = namespace.add = function(content) {
        items.push(content);
        
        if (!cycling) {
            displayNext();
        }
    };

    var getNext = namespace.getNext = function() {
        var result = (items.length == 0) ? ($idleContent) : (items.shift());

        return result;
    };

    var displayNext = namespace.displayNext = function() {
        var $content = getNext();

        setContent($content);

        if ($content == $idleContent) {
            cycling = false;
            return;
        }

        cycling = true;
        setTimeout(displayNext, timeout);
    };

    var setContent = namespace.setContent = function ($content) {
        $container.html($content);
    };

})(window.roomie.ui.notifications);