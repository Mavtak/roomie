import template from './template.html';

function networkWidget(
  $http
) {

  return {
    restrict: 'E',
    scope: {
      network: '=network',
      showEdit: '=showEdit'
    },
    template: template,
    link: link,
  };

  function link(scope) {
    scope.addDevice = addDevice;
    scope.removeDevice = removeDevice;

    function addDevice() {
      $http.post('/api/network', {
        action: 'addDevice',
        parameters: {
          id: scope.network.id
        }
      });
    }

    function removeDevice() {
      $http.post('/api/network', {
        action: 'removeDevice',
        parameters: {
          id: scope.network.id
        }
      });
    }
  }

}

export default networkWidget;
