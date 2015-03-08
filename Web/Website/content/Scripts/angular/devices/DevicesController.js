var module = angular.module('roomie.devices');

module.controller('DevicesController', ['$http', '$scope', '$timeout', function($http, $scope, $timeout) {

  initScope();

  forever(function() {
    return getData()
      .success(applyUpdates)
      .then(wait);
  });

  function initScope() {
    $scope.page = {
      items: []
    };
  }

  function forever(promiseFactory) {
    return promiseFactory().then(function() {
      forever(promiseFactory);
    });
  }

  function getData() {
    return $http.get('/api/device');
  }

  function applyUpdates(updates) {
    var originals = $scope.page.items;

    for (var i = 0; i < updates.length; i++) {
      var update = updates[i];
      var original = getOriginal(originals, update);

      if (original === update) {
        addOriginal(originals, original);
      } else {
        applyUpdate(original, update);
      }
    }
  }

  function wait() {
    return $timeout(function() {
    }, 500);
  }

  function getOriginal(originals, update) {
    for (var i = 0; i < originals.length; i++) {
      var original = originals[i];

      if (original.id === update.id) {
        return original;
      }
    }

    return update;
  }

  function addOriginal(originals, item) {
    setFunctions(item);
    originals.push(item);
  }

  function setFunctions(device) {
    device.binarySwitch.setPower = function(power) {
      $http.post('/api/device/' + device.id + '?action=Power' + power);
    };
  }

  function applyUpdate(original, update) {
    //todo: copy updated data from update to original
  }

}]);