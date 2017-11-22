import template from './template.html';

function deviceEditControls(
  $http,
  deviceTypes
) {

  return {
    restrict: 'E',
    scope: {
      device: '=device'
    },
    link: link,
    template: template,
  };

  function link(scope) {
    scope.model = {
      name: scope.device.name
    };

    if (scope.device.location) {
      scope.model.location = scope.device.location.name;
    }

    if (scope.device.type) {
      scope.model.type = scope.device.type.name;
    }

    scope.types = deviceTypes;

    scope.save = function () {
      $http.post('/api/device', {
        action: 'update',
        parameters: Object.assign({
          id: scope.device.id
        }, scope.model),
      });
    };
  }

}

export default deviceEditControls;
