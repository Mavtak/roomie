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
        '<current-action-controls ' +
          'current-action="device.currentAction" ' +
          'ng-if="!!device.currentAction"' +
          '>' +
        '</current-action-controls>' +
        '<multilevel-sensor-controls ' +
          'label="Temperature" ' +
          'ng-if="!!device.temperatureSensor.value && !device.hasThermostat()" ' +
          'sensor="device.temperatureSensor" ' +
          '>'+
        '</multilevel-sensor-controls>' +
        '<multilevel-sensor-controls ' +
          'label="Humidity" ' +
          'ng-if="!!device.humiditySensor.value" ' +
          'sensor="device.humiditySensor" ' +
          '>' +
        '</multilevel-sensor-controls>' +
        '<multilevel-sensor-controls ' +
          'label="Illuminance" ' +
          'ng-if="!!device.illuminanceSensor.value" ' +
          'sensor="device.illuminanceSensor" ' +
          '>' +
        '</multilevel-sensor-controls>' +
        '<multilevel-sensor-controls ' +
          'label="Power" ' +
          'ng-if="!!device.powerSensor.value" ' +
          'sensor="device.powerSensor" ' +
          '>' +
        '</multilevel-sensor-controls>' +
        '<binary-sensor-controls ' +
          'ng-if="!!device.binarySensor.timeStamp" ' +
          'sensor="device.binarySensor" ' +
          '>' +
        '</binary-sensor-controls>' +
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