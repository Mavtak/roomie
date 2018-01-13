import template from './template.html';

function thermostatControls() {

  return {
    restrict: 'E',
    scope: {
      temperatureSensor: '=temperatureSensor',
      thermostat: '=thermostat'
    },
    template: template,
  };

}

export default thermostatControls;
