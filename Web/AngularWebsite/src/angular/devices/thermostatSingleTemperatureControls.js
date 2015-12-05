var module = angular.module('roomie.devices');

module.directive('thermostatSingleTemperatureControls', function() {

  return {
    restrict: 'E',
    scope: {
      'label': '@label',
      'set': '=set',
      'temperature': '=temperature'
    },
    link: link,
    template: '' +
      '<div ' +
        'class="temperature"' +
        '>' +
        '<button ' +
          'class="button setpoint-button"' +
          'ng-click="colder()" ' +
          'ng-if="canSet()" ' +
          '>' +
          '-' +
        '</button>' +
        '<div ' +
          'class="value"' +
          '>' +
          '{{format(temperature)}}' +
        '</div>' +
        '<button ' +
          'class="button setpoint-button"' +
          'ng-click="hotter()" ' +
          'ng-if="canSet()" ' +
          '>' +
          '+' +
        '</button>' +
        '<div ' +
          'class="description"' +
          '>' +
          '{{label}}' +
        '</div>' +
      '</div>'
  };

  function link(scope) {
    scope.canSet = canSet;
    scope.colder = colder;
    scope.format = format;
    scope.hotter = hotter;

    function canSet() {
      if (typeof scope.set !== 'function') {
        return false;
      }

      if (typeof scope.temperature === 'undefined') {
        return false;
      }

      if (typeof scope.temperature.value === 'undefined') {
        return false;
      }

      return true;
    }

    function colder() {
      var newTemperature = add(scope.temperature, -1);
      scope.set(newTemperature);
    }

    function hotter() {
      var newTemperature = add(scope.temperature, 1);
      scope.set(newTemperature);
    }
  }

  function add(temperature, amount) {
    return {
      value: temperature.value + amount,
      units: temperature.units
    };
  }

  function format(temperature) {
    if (typeof temperature === 'undefined' || typeof temperature.value === 'undefined') {
      return '';
    }

    var result = temperature.value;

    if (typeof temperature.units === 'string') {
      result += '°' + temperature.units.substr(0, 1);
    }

    return result;
  }
});
