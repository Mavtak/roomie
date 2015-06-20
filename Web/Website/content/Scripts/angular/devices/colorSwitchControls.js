var module = angular.module('roomie.devices');

module.directive('colorSwitchControls', ['ColorSwitchButtonGenerator', function (ColorSwitchButtonGenerator) {
  return {
    restrict: 'E',
    scope: {
      colorSwitch: '=colorSwitch'
    },
    link: link,
    template: '' +
      '<widget-button-group>' +
        '<widget-button ' +
          'ng-repeat="button in buttons" ' +
          'label="{{button.label}}" ' +
          'activate="colorSwitch.setValue(button.color)" ' +
          'activated="button.activated" ' +
          'color="{{button.color}}"' +
          '>' +
          '</widget-button>' +
      '</widget-button-group>'
    };
  
  function link(scope) {
    var buttonGenerator = new ColorSwitchButtonGenerator(scope.colorSwitch);

    updateButtons();

    scope.$watch('colorSwitch.color.value', updateButtons);
    
    function updateButtons() {
      scope.buttons = buttonGenerator.generate();
    }
  }
}]);
