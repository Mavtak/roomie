var module = angular.module('roomie.devices');

module.directive('deviceWidget', function() {
  return {
    restrict: 'E',
    scope: {
      device: '=device'
    },
    template: '' +
      '<widget>' +
        '<widget-header ' +
          'title="{{device.name}} "' +
          'href="#/devices/{{device.id}}"' +
          '>' +
        '</widget-header>' +
        '<binary-switch-controls ' +
          'binary-switch="device.binarySwitch" ' +
          'ng-if="!!device.binarySwitch.power"' +
          '>' +
        '</binary-switch-controls>' +
        '<multilevel-switch-controls ' +
          'multilevel-switch="device.multilevelSwitch" ' +
          'ng-if="!!device.multilevelSwitch.power || device.multilevelSwitch.power === 0"' +
          '>' +
        '</multilevel-switch-controls>' +
        '<thermostat-controls ' +
          'ng-if="device.hasThermostat()" ' +
          'temperature-sensor="device.temperatureSensor" ' +
          'thermostat="device.thermostat"' +
          '>' +
        '</thermostat-controls>' +
      '</widget>'
  };
});