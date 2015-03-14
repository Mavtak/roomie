var module = angular.module('roomie.devices');

module.directive('multilevelSwitchControls', ['MultilevelSwitchButtonGenerator', function(MultilevelSwitchButtonGenerator) {
  return {
    restrict: 'E',
    scope: {
      multilevelSwitch: '=multilevelSwitch'
    },
    link: link,
    template: '' +
      '<widget-button-group>' +
        '<widget-button ' +
          'ng-repeat="button in buttons" ' +
          'label="{{button.label}}" ' +
          'activate="multilevelSwitch.setPower(button.power)" ' +
          'activated="button.activated" ' +
          '>' +
          '</widget-button>' +
      '</widget-button-group>'
    };
  
  function link(scope) {
    var buttonGenerator = new MultilevelSwitchButtonGenerator(scope.multilevelSwitch);

    updateButtons();

    scope.$watch('multilevelSwitch.power', updateButtons);
    
    function updateButtons() {
      scope.buttons = buttonGenerator.generate(11);
    }
  }
}]);
