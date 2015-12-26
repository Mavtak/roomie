var module = angular.module('roomie.devices');

module.directive('colorSwitchControls', ['ColorSwitchButtonGenerator', function (ColorSwitchButtonGenerator) {
  return {
    restrict: 'E',
    scope: {
      colorSwitch: '=colorSwitch'
    },
    link: link,
    templateUrl: 'devices/color-switch-controls/template.html',
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
