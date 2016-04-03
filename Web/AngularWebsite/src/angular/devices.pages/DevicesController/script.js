angular.module('roomie.devices.pages').controller('DevicesController', function (
  $http,
  $scope,
  $state,
  AutomaticPollingUpdater,
  LocationHeaderLabelGenerator,
  pageMenuItems,
  signInState,
  wholePageStatus
) {

  var locations;

  wholePageStatus.set('loading');
  pageMenuItems.reset();
  initializeScope();
  connectData();

  $scope.$watchCollection(function () { return locations; }, updatePageMenuItems, true);

  function initializeScope() {
    $scope.page = {
      items: []
    };
    $scope.include = shouldShowDevice;
  }

  function calculateLocations() {
    var result = _.map($scope.page.items, function (device) {
      if (typeof device.location === 'object') {
        return device.location.name;
      }

      return '';
    });

    return result;
  }

  function calculatePageMenuItems() {
    var previous = '';
    var locationData = _.map(locations, function (current) {
      var generator = new LocationHeaderLabelGenerator(previous, current);
      var parts = generator.getParts();
      previous = current;

      return parts;
    });
    locationData = _.flatten(locationData);

    var result = _.map(locationData, function (item) {
      return {
        indent: item.depth,
        label: item.label,
        target: '#/devices?location=' + item.location
      };
    });

    return result;
  }

  function connectData() {
    var path = '/api/device';

    if ($state.params.examples) {
      path += "?examples=true";
    }

    var options = {
      url: path,
      originals: $scope.page.items,
      ammendOriginal: setFunctions,
      processErrors: processErrors,
      processUpdate: processUpdate,
      updateComplete: updateComplete
    };

    if (typeof $state.params.id === 'undefined') {
      options.itemSelector = selectItemsFromList;
    } else {
      options.url += '/' + $state.params.id;
      options.itemSelector = selectItemFromDetail;
    }

    var data = new AutomaticPollingUpdater(options);

    data.run();

    $scope.$on('$destroy', function () {
      data.stop();
    });
  }

  function processErrors(errors) {
    wholePageStatus.set('ready');

    var signInError = _.isArray(errors) && _.some(errors, {
      type: 'must-sign-in'
    });

    if (signInError) {
      signInState.set('signed-out');
    }

    //TODO: handle other errors
  }

  function processUpdate(device) {
    if (device.binarySensor.timeStamp) {
      device.binarySensor.timeStamp = new Date(device.binarySensor.timeStamp);
    }

    if (device.temperatureSensor.timeStamp) {
      device.temperatureSensor.timeStamp = new Date(device.temperatureSensor.timeStamp);
    }

    if (device.humiditySensor.timeStamp) {
      device.humiditySensor.timeStamp = new Date(device.humiditySensor.timeStamp);
    }

    if (device.illuminanceSensor.timeStamp) {
      device.illuminanceSensor.timeStamp = new Date(device.illuminanceSensor.timeStamp);
    }

    if (device.powerSensor.timeStamp) {
      device.powerSensor.timeStamp = new Date(device.powerSensor.timeStamp);
    }
  }

  function selectItemsFromList(items) {
    return items;
  }

  function selectItemFromDetail(item) {
    return [item];
  }

  function setFunctions(device) {
    wholePageStatus.set('ready');
    signInState.set('signed-in');

    device.binarySwitch.setPower = function (power) {
      $http.post('/api/device/' + device.id + '?action=Power' + power);
    };

    device.multilevelSwitch.setPower = function (power) {
      $http.post('/api/device/' + device.id + '?action=Dim&power=' + power);
    };

    device.colorSwitch.setValue = function (color) {
      $http.post('/api/device/' + device.id + '?action=SetColor&color=' + encodeURIComponent(color));
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

    device.hasThermostat = function () {
      return hasThermostat(device);
    };

    device.binarySensor.poll = function () {
      $http.post('/api/device/' + device.id + '?action=PollBinarySensor');
    };

    device.temperatureSensor.poll = function () {
      $http.post('/api/device/' + device.id + '?action=PollTemperatureSensor');
    };

    device.humiditySensor.poll = function () {
      $http.post('/api/device/' + device.id + '?action=PollHumiditySensor');
    };

    device.illuminanceSensor.poll = function () {
      $http.post('/api/device/' + device.id + '?action=PollIlluminanceSensor');
    };

    device.powerSensor.poll = function () {
      $http.post('/api/device/' + device.id + '?action=PollPowerSensor');
    };
  }

  function shouldShowDevice(device) {
    var location = $state.params.location;

    if (typeof location !== 'undefined' && location !== '') {
      if (typeof device.location === 'undefined') {
        return false;
      }

      if (typeof device.location.name !== 'string') {
        return false;
      }

      if (device.location.name.indexOf(location) !== 0 && location.indexOf(device.location.name) !== 0) {
        return false;
      }
    }

    return true;
  }

  function updateComplete() {
    locations = calculateLocations();
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

  function updatePageMenuItems() {
    var items = calculatePageMenuItems();

    pageMenuItems.set(items);
  }

});
