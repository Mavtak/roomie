function thermostatTemperatureControls(
  getNewModeToToggleSetpoint
) {

  return {
    restrict: 'E',
    scope: {
      'temperature': '=temperature',
      'setpoints': '=setpoints',
      'core': '=core',
    },
    link: link,
    templateUrl: 'devices/thermostat-temperature-controls/template.html',
  };

  function link(scope) {
    scope.setCool = setCool;
    scope.setHeat = setHeat;
    scope.toggleCool = toggleCool;
    scope.toggleHeat = toggleHeat;

    function setCool(temperature) {
      scope.setpoints.set('cool', temperature);
    }

    function setHeat(temperature) {
      scope.setpoints.set('heat', temperature);
    }

    function toggleCool() {
      toggleSetpoint('cool');
    }

    function toggleHeat() {
      toggleSetpoint('heat');
    }

    function toggleSetpoint(toggledSetpoint) {
      var currentMode = scope.core.mode;
      var newMode = getNewModeToToggleSetpoint(currentMode, toggledSetpoint);

      if (newMode === undefined) {
        throw new Error('could not toggle setpoint "' + toggledSetpoint + '" with a current mode of "' + currentMode + '".');
      }

      scope.core.set(newMode);
    }
  }

}

export default thermostatTemperatureControls;
