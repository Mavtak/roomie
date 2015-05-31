var module = angular.module('roomie.common');

module.directive('roomieApp', ['$window', function($window) {

  return {
    restrict: 'E',
    link: link,
    template: '' +
      '<div id="page">' +
        '<dock ' +
          'area="top"' +
          'pixel-height="heights.topDock" ' +
          '>' +
          '<app-horizontal-section ' +
            'row-id="headerRow" ' +
            '>' +
            '<app-header ' +
              'navigation-menu="navigationMenu" ' +
              '>' +
            '</app-header>' +
          '</app-horizontal-section>' +
        '</dock>' +
        '<app-horizontal-section ' +
          'row-id="contentRow" ' +
          '>' +
          '<side-menu ' +
            'ng-show="navigationMenu.isOpen" ' +
            'calculated-width="navigationMenu.calculatedWidth" ' +
            'item-selected="navigationMenuItemSelected()" ' +
            '>' +
          '</side-menu>' +
          '<app-content ' +
            'ng-style="contentStyle" ' +
            '>' +
          '</app-content>' +
        '</app-horizontal-section>' +
        '<dock ' +
          'area="bottom"' +
          'pixel-height="heights.bottomDock" ' +
          '>' +
          '<app-horizontal-section ' +
            'row-id="footerRow" ' +
            '>' +
            '<app-footer' +
              '>' +
            '</app-footer>' +
          '</app-horizontal-section>' +
        '</dock>'
  };

  function link(scope) {
    scope.contentStyle = {};
    scope.heights = {
      app: 0,
      bottomDock: 0,
      topDock: 0
    };
    scope.navigationMenu = {
      close: closeNavigationMenu,
      isOpen: false,
      open: openNavigationMenu
    };

    scope.navigationMenuItemSelected = closeNavigationMenu;

    scope.$watch(calculateHeight, updateHeight);
    scope.$watch('heights', updateContentMinHeight, true);

    angular.element($window).bind('resize', function() {
      scope.$apply();
    });

    function calculateHeight() {
      return $window.innerHeight;
    }

    function updateHeight(newValue) {
      scope.heights.app = newValue;
    }

    function openNavigationMenu() {
      scope.contentStyle.left = scope.navigationMenu.calculatedWidth;
      scope.navigationMenu.isOpen = true;
    };

    function closeNavigationMenu() {
      delete scope.contentStyle.left;
      scope.navigationMenu.isOpen = false;
    };

    function updateContentMinHeight() {
      scope.contentStyle['box-sizing'] = 'border-box';
      scope.contentStyle['min-height'] = (scope.heights.app - scope.heights.topDock - scope.heights.bottomDock) + 'px';
    }
  }

}]);
