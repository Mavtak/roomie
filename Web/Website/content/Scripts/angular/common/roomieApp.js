var module = angular.module('roomie.common');

module.directive('roomieApp', function() {

  return {
    restrict: 'E',
    link: link,
    template: '' +
      '<div id="page">' +
        '<app-horizontal-section ' +
          'row-id="headerRow" ' +
          '>' +
          '<app-header ' +
            'navigation-menu="navigationMenu" ' +
            '>' +
          '</app-header>' +
        '</app-horizontal-section>' +
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
        '<app-horizontal-section ' +
          'row-id="footerRow" ' +
          '>' +
          '<app-footer' +
            '>' +
          '</app-footer>' +
        '</div>' +
      '</app-horizontal-section>'
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
