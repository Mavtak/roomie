var module = angular.module('roomie.devices');

module.directive('deviceWidget', function() {
  return {
    restrict: 'E',
    scope: {
      device: '=device'
    },
    templateUrl: 'devices/device-widget/template.html',
  };
});
