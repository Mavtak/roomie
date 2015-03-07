var module = angular.module('roomie.devices');

module.controller('DevicesController', ['$http', '$scope', function($http, $scope) {

  $http.get('/api/device').success(function (devices) {
    $scope.page = {
      items: devices
    };
  });

}]);