import template from './template.html';

function thermostatModeControls() {

  return {
    restrict: 'E',
    scope: {
      'label': '@label',
      'modes': '=modes'
    },
    link: link,
    template: template,
  };

  function link(scope) {
    scope.capitalizeFirstLetter = capitalizeFirstLetter;
    scope.formatCurrentAction = formatCurrentAction;

    function formatCurrentAction() {
      var action = scope.modes.currentAction;

      if (typeof action !== 'string' || action === '') {
        return '';
      }

      return 'Currently ' + capitalizeFirstLetter(action);
    }
  }

  function capitalizeFirstLetter(string) {
    return string.charAt(0).toUpperCase() + string.slice(1);
  }

}

export default thermostatModeControls;
