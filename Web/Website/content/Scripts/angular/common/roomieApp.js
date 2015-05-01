var module = angular.module('roomie.common');

module.directive('roomieApp', function() {

  return {
    restrict: 'E',
    link: link,
    template: '' +
      '<div id="page">' +
        '<dock ' +
          'area="top"' +
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
    scope.navigationMenu = {
      close: closeNavigationMenu,
      isOpen: false,
      open: openNavigationMenu
    };

    scope.navigationMenuItemSelected = closeNavigationMenu;

    function openNavigationMenu() {
      scope.contentStyle.left = scope.navigationMenu.calculatedWidth;
      scope.navigationMenu.isOpen = true;
    };

    function closeNavigationMenu() {
      delete scope.contentStyle.left;
      scope.navigationMenu.isOpen = false;
    };
  }

});
