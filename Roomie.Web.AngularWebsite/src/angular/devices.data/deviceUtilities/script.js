angular.module('roomie.devices.data').factory('deviceUtilities', function (
  $http
) {

  return new DeviceUtilities();

  function DeviceUtilities() {
    this.parseTimestamps = parseTimestamps;
    this.setActions = setActions;

    function buildUrl(parts) {
      var path = _.map(parts.path, function (value) {
        return encodeURIComponent(value);
      }).join('/');

      var querystring = _.map(parts.querystring, function (value, key) {
        return encodeURIComponent(key) + '=' + encodeURIComponent(value);
      }).join('&');

      return '/' + path + '?' + querystring;
    }

    function doAction(device, action, args) {
      var url = buildUrl({
        path: [
          'api',
          'device',
          device.id,
        ],
        querystring: _.extend({
          action: action
        }, args)
      });

      $http.post(url);
    }

    function parseTimestamp(device, property) {
      if (typeof device[property] === 'undefined' || typeof device[property].timeStamp === 'undefined') {
        return;
      }

      device[property].timeStamp = new Date(device[property].timeStamp);
    }

    function parseTimestamps(device) {
      parseTimestamp(device, 'binarySensor');
      parseTimestamp(device, 'humiditySensor');
      parseTimestamp(device, 'illuminanceSensor');
      parseTimestamp(device, 'powerSensor');
      parseTimestamp(device, 'temperatureSensor');
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

      if (typeof thermostat.setpoints !== 'undefined') {
        if (typeof thermostat.setpoints.cool !== 'undefined') {
          return true;
        }

        if (typeof thermostat.setpoints.heat !== 'undefined') {
          return true;
        }
      }

      return false;
    }

    function setActions(device) {

      if (device.binarySensor) {
        device.binarySensor.poll = function () {
          doAction(device, 'PollBinarySensor');
        };
      }

      if (device.binarySwitch) {
        device.binarySwitch.setPower = function (power) {
          doAction(device, 'Power' + power);
        };
      }

      if (device.colorSwitch) {
        device.colorSwitch.setValue = function (color) {
          doAction(device, 'SetColor', {
            color: color,
          });
        };
      }

      if (device.humiditySensor) {
        device.humiditySensor.poll = function () {
          doAction(device, 'PollHumiditySensor');
        };
      }

      if (device.illuminanceSensor) {
        device.illuminanceSensor.poll = function () {
          doAction(device, 'PollIlluminanceSensor');
        };
      }

      if (device.multilevelSwitch) {
        device.multilevelSwitch.setPower = function (power) {
          doAction(device, 'Dim', {
            power: power,
          });
        };
      }

      if (device.powerSensor) {
        device.powerSensor.poll = function () {
          doAction(device, 'PollPowerSensor');
        };
      }

      if (device.temperatureSensor) {
        device.temperatureSensor.poll = function () {
          doAction(device, 'PollTemperatureSensor');
        };
      }

      if (device.thermostat) {
        if (device.thermostat.core) {
          device.thermostat.core.set = function (mode) {
            doAction(device, 'SetThermostatMode', {
              mode: mode,
            });
          };
        }

        if (device.thermostat.fan) {
          device.thermostat.fan.set = function (mode) {
            doAction(device, 'SetThermostatFanMode', {
              mode: mode,
            });
          };
        }

        if (device.thermostat.setpoints) {
          device.thermostat.setpoints.set = function (type, temperature) {
            doAction(device, 'SetThermostatSetpoint', {
              type: type,
              temperature: temperature.value + ' ' + temperature.units,
            });
          };
        }
      }

      device.hasThermostat = function () {
        return hasThermostat(device);
      };
    }

    function thermostatHasModes(modes) {
      if (typeof modes === 'undefined') {
        return false;
      }

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
  }

});
