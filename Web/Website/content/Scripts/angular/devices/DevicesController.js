var module = angular.module('roomie.devices');

module.controller('DevicesController', ['$http', '$scope', function($http, $scope) {

  $http.get('/api/device').success(function (devices) {
    $scope.page = {
      items: devices
    };

    for (var i = 0; i < $scope.page.items.length; i++) {
      processDevices($scope.page.items[i]);
    }
  });

  function processDevices(device) {
    device.binarySwitch.setPower = function(power) {
      $http.post('/api/device/' + device.id + '?action=Power' + power);
    };
  }

}]);