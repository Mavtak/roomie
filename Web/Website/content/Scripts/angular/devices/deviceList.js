var module = angular.module('roomie.devices');

module.directive('deviceList', function() {

  return {
    restrict: 'E',
    scope: {
      devices: '=devices'
    },
    template: '' +
      '<div ' +
        'ng-repeat="device in devices"' +
        '>' +
        '<location-header-group ' +
          'previous-location="devices[$index - 1].location.name" ' +
          'current-location="device.location.name"' +
          '>' +
        '</location-header-group>' +
        '<device-widget ' +
          'device="device" ' +
          '>' +
        '</device-widget>' +
      '</div>'
  };

});