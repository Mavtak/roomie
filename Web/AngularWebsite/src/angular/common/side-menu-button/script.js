var module = angular.module('roomie.common');

module.directive('sideMenuButton', function() {

  return {
    restrict: 'E',
    scope: {
      'close': '&close',
      'isOpen': '=isOpen',
      'open': '&open'
    },
    link: link,
    templateUrl: 'common/side-menu-button/template.html',
  };

  function link(scope) {
    scope.isOpen = false;
    scope.toggle = toggle;

    function toggle() {
      var action = scope.isOpen ? scope.close : scope.open;
      scope.isOpen = !scope.isOpen;

      if (typeof action === 'function') {
        action();
      }
    }
  }

});
