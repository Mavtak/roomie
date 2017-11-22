function runScriptControls(
  $http
) {

  return {
    restrict: 'E',
    scope: {
      computer: '=computer'
    },
    templateUrl: 'computers/run-script-controls/template.html',
    link: link
  };

  function link(scope) {
    scope.model = {};
    scope.runScript = runScript;

    waitUntilLoaded(scope, 'computer', function () {
      if (scope.computer && scope.computer.lastScript) {
        scope.model.script = scope.computer.lastScript.text;
      } else {
        scope.model.script = '<Computer.Speak Text="Hi there!" />';
      }
    });

    function runScript() {
      $http.post('/api/computer', {
        action: 'runScript',
        parameters: Object.assign({}, {
           id: scope.computer.id
        }, scope.model),
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

export default runScriptControls;
