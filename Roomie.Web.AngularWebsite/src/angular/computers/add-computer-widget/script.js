import template from './template.html';

function addComputerWidget(
  $http
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
      $http.post('/api/computer', {
        action: 'create',
        parameters: scope.model,
      });
    }
  }

}

export default addComputerWidget;
