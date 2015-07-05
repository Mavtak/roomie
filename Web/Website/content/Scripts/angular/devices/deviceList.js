var module = angular.module('roomie.devices');

module.directive('deviceList', function() {

  return {
    restrict: 'E',
    scope: {
      devices: '=devices',
      include: '=include'
    },
    link: link,
    template: '' +
      '<div ' +
        'ng-repeat="device in filteredDevices"' +
        '>' +
        '<location-header-group ' +
          'previous-location="filteredDevices[$index - 1].location.name" ' +
          'current-location="device.location.name"' +
          '>' +
        '</location-header-group>' +
        '<device-widget ' +
          'device="device" ' +
          '>' +
        '</device-widget>' +
      '</div>'
  };

  function link(scope) {
    scope.$watch('devices', filterDevices, true);

    filterDevices();

    function filterDevices() {
      if (typeof scope.include === 'undefined') {
        return scope.filteredDevices = scope.devices;
      }

      scope.filteredDevices = _.filter(scope.devices, scope.include);
    }
  }

});