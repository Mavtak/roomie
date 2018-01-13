import template from './template.html';

function addComputerWidget(
  api
) {

  return {
    restrict: 'E',
    scope: {},
    template: template,
    link: link
  };

  function link(scope) {
    scope.add = add;
    scope.model = {};

    function add() {
      api({
        repository: 'computer',
        action: 'create',
        parameters: scope.model,
      });
    }
  }

}

export default addComputerWidget;
