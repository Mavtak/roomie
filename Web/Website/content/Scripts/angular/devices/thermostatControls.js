﻿var module = angular.module('roomie.devices');

module.directive('thermostatControls', function () {

  return {
    restrict: 'E',
    scope: {
      temperatureSensor: '=temperatureSensor',
      thermostat: '=thermostat'
    },
    template: '' +
      '<thermostat-temperature-controls ' +
        'ng-if="!!thermostat.setpoints.cool || !!thermostat.setpoints.heat || !!temperatureSensor.value" ' +
        'temperature="temperatureSensor.value" ' +
        'setpoints="thermostat.setpoints"' +
        '>' +
      '</thermostat-temperature-controls>' +
      '<thermostat-mode-controls ' +
        'label="System Mode" ' +
        'modes="thermostat.core" ' +
        'ng-if="!!thermostat.core.currentAction || !!thermostat.core.mode || thermostat.core.supportedModes.length > 0"' +
        '>' +
      '</thermostat-mode-controls>' +
      '<thermostat-mode-controls ' +
        'label="Fan Mode" ' +
        'modes="thermostat.fan" ' +
        'ng-if="!!thermostat.fan.currentAction || !!thermostat.fan.mode || thermostat.fan.supportedModes.length > 0"' +
        '>' +
      '</thermostat-mode-controls>'
  };

});