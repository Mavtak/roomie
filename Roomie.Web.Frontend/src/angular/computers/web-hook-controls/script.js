import template from './template.html';

function webHookControls(
  $state,
  $window,
  api
) {

  return {
    restrict: 'E',
    scope: {
      computer: '=computer'
    },
    template: template,
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

      return '<WebHook.Connect ComputerName="' + scope.computer.name + '" CommunicationURL="' + $window.location.origin + '/api/" AccessKey="' + scope.computer.accessKey + '" EncryptionKey="' + scope.computer.encryptionKey + '" />';
    }

    function disable() {
      api({
        repository: 'computer',
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
      api({
        repository: 'computer',
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
