var module = angular.module('roomie.common');

module.directive('appHeader', function () {

  return {
    restrict: 'E',
    scope: {
      navigationMenu: '=navigationMenu',
    },
    template: '' +
      '<div id="header">' +
        '<side-menu-button ' +
          'close="navigationMenu.close()"' +
          'is-open="navigationMenu.isOpen" ' +
          'open="navigationMenu.open()" ' +
          '>' +
        '</side-menu-button>' +
        '<h1 id="title">Roomie</h1>' +
      '</div>'
  };

});
