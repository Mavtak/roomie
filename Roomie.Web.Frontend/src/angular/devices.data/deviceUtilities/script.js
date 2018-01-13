function deviceUtilities(
  api
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
      api({
        repository: 'device',
        action: action,
        parameters: Object.assign({
          id: device.id,
        }, args),
      });
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
          doAction(device, 'binarySensorPoll');
        };
      }

      if (device.binarySwitch) {
        device.binarySwitch.setPower = function (power) {
          doAction(device, 'binarySwitchSetPower', {
            power: power
          });
        };
      }

      if (device.colorSwitch) {
        device.colorSwitch.setValue = function (color) {
          doAction(device, 'colorSwitchSetValue', {
            color: color,
          });
        };
      }

      if (device.humiditySensor) {
        device.humiditySensor.poll = function () {
          doAction(device, 'humiditySensorPoll');
        };
      }

      if (device.illuminanceSensor) {
        device.illuminanceSensor.poll = function () {
          doAction(device, 'illuminanceSensorPoll');
        };
      }

      if (device.multilevelSwitch) {
        device.multilevelSwitch.setPower = function (power) {
          doAction(device, 'multilevelSwitchSetPower', {
            power: power,
          });
        };
      }

      if (device.powerSensor) {
        device.powerSensor.poll = function () {
          doAction(device, 'powerSensorPoll');
        };
      }

      if (device.temperatureSensor) {
        device.temperatureSensor.poll = function () {
          doAction(device, 'temperatureSensorPoll');
        };
      }

      if (device.thermostat) {
        if (device.thermostat.core) {
          device.thermostat.core.set = function (mode) {
            doAction(device, 'thermostatCoreSetMode', {
              mode: mode,
            });
          };
        }

        if (device.thermostat.fan) {
          device.thermostat.fan.set = function (mode) {
            doAction(device, 'thermostatFanSetMode', {
              mode: mode,
            });
          };
        }

        if (device.thermostat.setpoints) {
          device.thermostat.setpoints.set = function (type, temperature) {
            doAction(device, 'thermostatSetpointsSetSetpoint', {
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

}

export default deviceUtilities;
