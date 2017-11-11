angular.module('roomie.networks').directive('networkWidget', function (
  $http
) {

  return {
    restrict: 'E',
    scope: {
      network: '=network',
      showEdit: '=showEdit'
    },
    templateUrl: 'networks/network-widget/template.html',
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

});
