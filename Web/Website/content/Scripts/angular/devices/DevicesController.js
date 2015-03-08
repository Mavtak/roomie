var module = angular.module('roomie.devices');

module.controller('DevicesController', ['$http', '$scope', 'AutomaticPollingUpdater', function ($http, $scope, AutomaticPollingUpdater) {

  initializeScope();
  connectData();
  
  function initializeScope() {
    $scope.page = {
      items: []
    };
  }

  function connectData() {
    var data = new AutomaticPollingUpdater({
      url: '/api/device',
      itemSelector: function (items) {
        return items;
      },
      originals: $scope.page.items,
      setFunctions: setFunctions
    });

    data.run();
  }
  
  function setFunctions(device) {
    device.binarySwitch.setPower = function (power) {
      $http.post('/api/device/' + device.id + '?action=Power' + power);
    };
  }

}]);