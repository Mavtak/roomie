var module = angular.module('roomie.devices');

module.directive('sensorControls', function() {

  return {
    restrict: 'E',
    scope: {
      label: '@label',
      sensor: '=sensor'
    },
    template: '' +
      '<div ' +
        'class="group" ' +
        '>' +
        '<button ' +
          'ng-click="sensor.poll()"' +
          '>' +
          '{{label}}: {{sensor.value.value}} {{sensor.value.units}} (at {{sensor.timeStamp.toLocaleString()}})' +
        '</button>' +
      '</div>'
  };

});
