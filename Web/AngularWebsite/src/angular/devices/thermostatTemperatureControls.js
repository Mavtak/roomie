var module = angular.module('roomie.devices');

module.directive('thermostatTemperatureControls', function() {

  return {
    restrict: 'E',
    scope: {
      'temperature': '=temperature',
      'setpoints': '=setpoints'
    },
    link: link,
    template: '' +
      '<div ' +
        'class="thermostat-controls" ' +
        '>' +
        '<thermostat-single-temperature-controls ' +
          'label="Heat" ' +
          'set="setHeat" ' +
          'temperature="setpoints.heat"' +
          '>' +
        '</thermostat-single-temperature-controls>' +
        '<thermostat-single-temperature-controls ' +
          'label="Current" ' +
          'temperature="temperature"' +
          '>' +
        '</thermostat-single-temperature-controls>' +
        '<thermostat-single-temperature-controls ' +
          'label="Cool" ' +
          'set="setCool" ' +
          'temperature="setpoints.cool"' +
          '>' +
        '</thermostat-single-temperature-controls>' +
      '</div>'
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
