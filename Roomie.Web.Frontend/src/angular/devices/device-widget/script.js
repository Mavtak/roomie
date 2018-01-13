import template from './template.html';

function deviceWidget() {

  return {
    restrict: 'E',
    scope: {
      device: '=device',
      showEdit: '=showEdit'
    },
    template: template,
    link: link
  };

  function link(scope) {
    scope.getSubtitle = getSubtitle;

    function getSubtitle() {
      if (scope.device && scope.device.thermostat && scope.device.thermostat.core && scope.device.thermostat.core.currentAction) {
        return 'Currently ' + capitalizeFirstLetter(scope.device.thermostat.core.currentAction);
      }
    }
  }

  function capitalizeFirstLetter(string) {
    return string.charAt(0).toUpperCase() + string.slice(1);
  }

}

export default deviceWidget;
