import template from './template.html';

function multilevelSensorControls() {

  return {
    restrict: 'E',
    scope: {
      label: '@label',
      sensor: '=sensor'
    },
    template: template,
  };

}

export default multilevelSensorControls;
