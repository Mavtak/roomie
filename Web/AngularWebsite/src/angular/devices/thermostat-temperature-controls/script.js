var module = angular.module('roomie.devices');

module.directive('thermostatTemperatureControls', function() {

  return {
    restrict: 'E',
    scope: {
      'temperature': '=temperature',
      'setpoints': '=setpoints'
    },
    link: link,
    templateUrl: 'devices/thermostat-temperature-controls/template.html',
  };

  function link(scope) {
    scope.setCool = setCool;
    scope.setHeat = setHeat;

    function setCool(temperature) {
      scope.setpoints.set('cool', temperature);
    }

    function setHeat(temperature) {
      scope.setpoints.set('heat', temperature);
    }
  }

});
