function thermostatSingleTemperatureControls(
  getNewModeToToggleSetpoint
) {

  return {
    restrict: 'E',
    scope: {
      'label': '@label',
      'set': '=set',
      'temperature': '=temperature',
      'toggle': '=toggle',
      'active': '=active'
    },
    link: link,
    templateUrl: 'devices/thermostat-single-temperature-controls/template.html',
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
      result += '\u00B0' + temperature.units.substr(0, 1);
    }

    return result;
  }

}

export default thermostatSingleTemperatureControls;
