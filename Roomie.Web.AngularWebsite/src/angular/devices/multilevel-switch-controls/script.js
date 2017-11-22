function multilevelSwitchControls(
  MultilevelSwitchButtonGenerator
  ) {

  return {
    restrict: 'E',
    scope: {
      multilevelSwitch: '=multilevelSwitch'
    },
    link: link,
    templateUrl: 'devices/multilevel-switch-controls/template.html',
    };

  function link(scope) {
    var buttonGenerator = new MultilevelSwitchButtonGenerator(scope.multilevelSwitch);

    updateButtons();

    scope.$watch('multilevelSwitch.power', updateButtons);

    function updateButtons() {
      scope.buttons = buttonGenerator.generate(11);
    }
  }

}

export default multilevelSwitchControls;
