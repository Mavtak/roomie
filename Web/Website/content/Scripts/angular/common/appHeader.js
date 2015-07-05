var module = angular.module('roomie.common');

module.directive('appHeader', ['pageMenuItems', function (pageMenuItems) {

  return {
    restrict: 'E',
    scope: {
      navigationMenu: '=navigationMenu',
      pageMenu: '=pageMenu',
    },
    link: link,
    template: '' +
      '<div id="header">' +
        '<side-menu-button ' +
          'close="navigationMenu.close()"' +
          'is-open="navigationMenu.isOpen" ' +
          'open="navigationMenu.open()" ' +
          '>' +
        '</side-menu-button>' +
        '<h1 id="title">Roomie</h1>' +
        '<side-menu-button ' +
          'ng-if="pageMenuItems.any()"' +
          'close="pageMenu.close()"' +
          'is-open="pageMenu.isOpen" ' +
          'open="pageMenu.open()" ' +
          'style="float: right;"' +
          '>' +
        '</side-menu-button>' +
      '</div>'
  };

  function link(scope) {
    scope.pageMenuItems = pageMenuItems;
  }
}]);
