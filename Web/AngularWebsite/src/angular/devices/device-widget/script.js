angular.module('roomie.devices').directive('deviceWidget', function () {
  return {
    restrict: 'E',
    scope: {
      device: '=device'
    },
    templateUrl: 'devices/device-widget/template.html',
  };
});
