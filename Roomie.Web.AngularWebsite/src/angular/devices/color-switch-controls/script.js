import template from './template.html';

function colorSwitchControls(
  ColorSwitchButtonGenerator
) {

  return {
    restrict: 'E',
    scope: {
      colorSwitch: '=colorSwitch'
    },
    link: link,
    template: template,
  };

  function link(scope) {
    var buttonGenerator = new ColorSwitchButtonGenerator(scope.colorSwitch);

    updateButtons();

    scope.$watch('colorSwitch.color.value', updateButtons);

    function updateButtons() {
      scope.buttons = buttonGenerator.generate();
    }
  }

}

export default colorSwitchControls;
