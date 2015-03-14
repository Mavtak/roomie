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
    var options = {
      url: '/api/device',
      originals: $scope.page.items,
      ammendOriginal: setFunctions
    };

    if (typeof $scope.$state.params.id === 'undefined') {
      options.itemSelector = selectItemsFromList;
    } else {
      options.url += '/' + $scope.$state.params.id;
      options.itemSelector = selectItemFromDetail;
    }

    var data = new AutomaticPollingUpdater(options);

    data.run();

    $scope.$on('$destroy', function() {
      data.stop();
    });
  }

  function selectItemsFromList(items) {
    return items;
  }
  
  function selectItemFromDetail(item) {
    return [item];
  }

  function setFunctions(device) {
    device.binarySwitch.setPower = function (power) {
      $http.post('/api/device/' + device.id + '?action=Power' + power);
    };

    device.multilevelSwitch.setPower = function (power) {
      $http.post('/api/device/' + device.id + '?action=Dim&power=' + power);
    };
  }

}]);