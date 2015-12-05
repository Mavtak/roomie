var module = angular.module('roomie.devices');

module.directive('thermostatModeControls', function() {

  return {
    restrict: 'E',
    scope: {
      'label': '@label',
      'modes': '=modes'
    },
    link: link,
    template: '' +
      '<div ' +
        'class="header" ' +
        '>' +
        '<div ' +
          'class="secondary" ' +
          '>' +
          '{{formatCurrentAction()}}' +
        '</div>' +
        '{{label}}' +
      '</div>' +
      '<widget-button-group ' +
        'ng-if="modes.supportedModes.length > 0"' +
        '>' +
        '<widget-button ' +
          'ng-repeat="mode in modes.supportedModes" ' +
          'label="{{capitalizeFirstLetter(mode)}}" ' +
          'activate="modes.set(mode)" ' +
          'activated="mode === modes.mode" ' +
          '>' +
        '</widget-button>' +
      '</widget-button-group>'
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
});
