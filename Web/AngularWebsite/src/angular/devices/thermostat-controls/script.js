angular.module('roomie.devices').directive('thermostatControls', function () {

  return {
    restrict: 'E',
    scope: {
      temperatureSensor: '=temperatureSensor',
      thermostat: '=thermostat'
    },
    templateUrl: 'devices/thermostat-controls/template.html',
  };

});
