function thermostatControls() {

  return {
    restrict: 'E',
    scope: {
      temperatureSensor: '=temperatureSensor',
      thermostat: '=thermostat'
    },
    templateUrl: 'devices/thermostat-controls/template.html',
  };

}

export default thermostatControls;
