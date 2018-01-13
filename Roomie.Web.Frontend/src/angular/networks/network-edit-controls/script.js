import template from './template.html';

function networkEditControls(
  $http
) {

  return {
    restrict: 'E',
    scope: {
      network: '=network'
    },
    link: link,
    template: template,
  };

  function link(scope) {
    scope.model = {};
    scope.save = save;

    waitUntilLoaded(scope, 'network', function () {
      scope.model.name = scope.network.name;
    });

    function save() {
      $http.post('/api/network', {
        action: 'update',
        parameters: scope.model,
      });
    }
  }

  function waitUntilLoaded(scope, variableName, callback) {
    var unbindWatch = scope.$watch(variableName, function (value) {
      if (value === undefined) {
        return;
      }

      unbindWatch();
      callback();
    });
  }

}

export default networkEditControls;
