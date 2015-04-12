var module = angular.module('roomie.common');

module.directive('appHeader', function () {

  return {
    restrict: 'E',
    template: '' +
      '<div id="header">' +
        '<h1 id="title">Roomie</h1>' +
      '</div>'
  };

});
