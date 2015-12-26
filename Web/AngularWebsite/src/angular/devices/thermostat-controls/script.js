var module = angular.module('roomie.devices');

module.directive('thermostatControls', function () {

  return {
    restrict: 'E',
    scope: {
      temperatureSensor: '=temperatureSensor',
      thermostat: '=thermostat'
    },
    templateUrl: 'devices/thermostat-controls/template.html',
  };

});
