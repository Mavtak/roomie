var module = angular.module('roomie.devices');

module.directive('deviceList', function() {

  return {
    restrict: 'E',
    scope: {
      devices: '=devices'
    },
    template: '' +
      '<device-widget ' +
        'device="device" ' +
        'ng-repeat="device in devices"' +
        '>' +
      '</device-widget>'
  };

});