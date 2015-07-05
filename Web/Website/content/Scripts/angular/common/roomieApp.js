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
              'page-menu="pageMenu" ' +
              '>' +
            '</app-header>' +
          '</app-horizontal-section>' +
        '</dock>' +
        '<app-horizontal-section ' +
          'row-id="contentRow" ' +
          '>' +
          '<side-menu-set ' +
            'top="heights.topDock" ' +
            'bottom="heights.bottomDock" ' +
            'width="widths.app" ' +
            '>' +
            '<side-menu ' +
              'ng-show="navigationMenu.isOpen" ' +
              'calculated-width="navigationMenu.calculatedWidth" ' +
              'side="left" ' +
              '>' +
              '<side-menu-item label="\'Devices\'" selected="navigationMenuItemSelected" target="\'#/devices\'"></side-menu-item>' +
              '<side-menu-item label="\'Tasks\'" selected="navigationMenuItemSelected" target="\'#/tasks\'"></side-menu-item>' +
            '</side-menu>' +
            '<side-menu ' +
              'ng-show="pageMenu.isOpen" ' +
              'calculated-width="pageMenu.calculatedWidth" ' +
              'item-selected="pageMenuItemSelected()" ' +
              'side="right" ' +
              '>' +
              '<side-menu-item label="\'Nothing Here\'" selected="pageMenuItemSelected"></side-menu-item>' +
              '</side-menu-item>' +
            '</side-menu>' +
          '</side-menu-set>' +
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
    scope.widths = {
      app: 0
    };
    scope.navigationMenu = {
      close: closeNavigationMenu,
      isOpen: false,
      open: openNavigationMenu
    };
    scope.pageMenu = {
      close: closePageMenu,
      isOpen: false,
      open: openPageMenu
    };

    scope.navigationMenuItemSelected = closeNavigationMenu;
    scope.pageMenuItemSelected = closePageMenu;

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

    function openPageMenu() {
      scope.contentStyle.right = scope.pageMenu.calculatedWidth;
      scope.pageMenu.isOpen = true;
    };

    function closeNavigationMenu() {
      delete scope.contentStyle.left;
      scope.navigationMenu.isOpen = false;
    };

    function closePageMenu() {
      delete scope.contentStyle.right;
      scope.pageMenu.isOpen = false;
    };

    function updateContentMinHeight() {
      scope.contentStyle['box-sizing'] = 'border-box';
      scope.contentStyle['min-height'] = (scope.heights.app - scope.heights.topDock - scope.heights.bottomDock) + 'px';
    }
  }

}]);
