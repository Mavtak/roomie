angular.module('roomie.devices').directive('multilevelSensorControls', function () {

  return {
    restrict: 'E',
    scope: {
      label: '@label',
      sensor: '=sensor'
    },
    templateUrl: 'devices/multilevel-sensor-controls/template.html',
  };

});
