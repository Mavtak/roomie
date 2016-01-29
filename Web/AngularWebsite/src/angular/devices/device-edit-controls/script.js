angular.module('roomie.devices').directive('deviceEditControls', function (
  $http,
  deviceTypes
) {
  return {
    restrict: 'E',
    scope: {
      device: '=device'
    },
    link: link,
    templateUrl: 'devices/device-edit-controls/template.html'
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
      $http.put('/api/device/' + scope.device.id, scope.model);
    };
  }
});
