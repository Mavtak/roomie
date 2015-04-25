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
      ammendOriginal: setFunctions,
      processUpdate: processUpdate
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

  function processUpdate(device) {
    if (device.temperatureSensor.timeStamp) {
      device.temperatureSensor.timeStamp = new Date(device.temperatureSensor.timeStamp);
    }
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

    device.thermostat.setpoints.set = function (type, temperature) {
      $http.post('/api/device/' + device.id + '?action=SetThermostatSetpoint&type=' + type + '&temperature=' + temperature.value + ' ' + temperature.units);
    };

    device.thermostat.core.set = function (mode) {
      $http.post('/api/device/' + device.id + '?action=SetThermostatMode&mode=' + mode);
    };
    
    device.thermostat.fan.set = function (mode) {
      $http.post('/api/device/' + device.id + '?action=SetThermostatFanMode&mode=' + mode);
    };

    device.hasThermostat = function() {
      return hasThermostat(device);
    };

    device.temperatureSensor.poll = function() {
      $http.post('/api/device/' + device.id + '?action=PollTemperatureSensor');
    };
  }

  function hasThermostat(device) {
    var thermostat = device.thermostat;

    if (typeof thermostat === 'undefined') {
      return false;
    }

    if (thermostatHasModes(thermostat.core)) {
      return true;
    }

    if (thermostatHasModes(thermostat.fan)) {
      return true;
    }

    if (typeof thermostat.setpoints.cool !== 'undefined') {
      return true;
    }

    if (typeof thermostat.setpoints.heat !== 'undefined') {
      return true;
    }

    return false;
  }

  function thermostatHasModes(modes) {
    if (typeof modes.currentAction !== 'undefined') {
      return true;
    }

    if (typeof modes.mode !== 'undefined') {
      return true;
    }

    if (typeof modes.supportedModes !== 'undefined' && modes.supportedModes.length > 0) {
      return true;
    }

    return false;
  }
}]);