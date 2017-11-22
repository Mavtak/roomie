function webHookControls(
  $http,
  $state,
  $window
) {

  return {
    restrict: 'E',
    scope: {
      computer: '=computer'
    },
    templateUrl: 'computers/web-hook-controls/template.html',
    link: link
  };

  function link(scope) {
    scope.disable = disable;
    scope.renew = renew;

    Object.defineProperty(scope, 'script', {
      get: calculateScript
    });

    function calculateScript() {
      if (!scope.computer || !scope.computer.accessKey || !scope.computer.encryptionKey) {
        return;
      }

      return '<WebHook.Connect ComputerName="' + scope.computer.name + '" CommunicationURL="' + $window.location.origin + '/communicator/" AccessKey="' + scope.computer.accessKey + '" EncryptionKey="' + scope.computer.encryptionKey + '" />';
    }

    function disable() {
      $http.post('/api/computer', {
        action: 'disableWebHook',
        parameters: {
          id: scope.computer.id,
        }
      })
        .then(function () {
          $state.reload();
        });
    }

    function renew() {
      $http.post('/api/computer', {
        action: 'renewWebHookKeys',
        parameters: {
          id: scope.computer.id,
        }
      })
        .then(function () {
          $state.reload();
        });
    }
  }

}

export default webHookControls;
