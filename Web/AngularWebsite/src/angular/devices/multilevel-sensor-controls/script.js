var module = angular.module('roomie.devices');

module.directive('multilevelSensorControls', function() {

  return {
    restrict: 'E',
    scope: {
      label: '@label',
      sensor: '=sensor'
    },
    templateUrl: 'devices/multilevel-sensor-controls/template.html',
  };

});
