var module = angular.module('roomie.common');

module.directive('widgetDisconnectedIcon', function() {

  return {
    restrict: 'E',
    template: '' +
      '<span class="icon disconnected">' +
        '<span class="layer">' +
          '<span class="pillar"></span>' +
          '<span class="dash"></span>' +
        '</span>' +
        '<span class="layer">' +
          '<span class="pillar"></span>' +
          '<span class="slashContainer">' +
            '<span class="slash"></span>' +
          '</span>' +
        '</span>' +
      '</span>'
  };

});
