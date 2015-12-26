describe('roomie.devices.deviceWidget', function() {
  var $compile;
  var $rootScope;
  var element;

  beforeEach(angular.mock.module('roomie.devices'));

  beforeEach(angular.mock.inject(function ($injector) {
    $compile = $injector.get('$compile');
    $rootScope = $injector.get('$rootScope');
  }));

  beforeEach(function() {
    $rootScope.device = {};

    element = $compile('<device-widget device="device"></device-widget>')($rootScope);
  });

  describe('the header', function() {

    it('has one', function() {
      expect($(element).find('.widget widget-header .header').length).toEqual(1);
    });

    it('has a title that matches the device name', function() {
      $rootScope.device.name = "Light Switch";

      $rootScope.$digest();

      expect($(element).find('.widget widget-header .header .name').html()).toEqual("Light Switch");
    });

    it('links to the detail', function () {
      $rootScope.device.id = '123';
      $rootScope.$digest();

      expect($(element).find('.widget widget-header .header').attr('href')).toEqual('#/devices/123');
    });

    it('has no subtitle', function() {
      $rootScope.$digest();

      expect($(element).find('.widget widget-header .header .location').html()).toEqual('');
    });

    it('sets the disconnected property', function() {
      $rootScope.device.isConnected = false;
      $rootScope.$digest();

      expect($(element).find('.widget widget-header > .header widget-disconnected-icon').length).toEqual(1);

      $rootScope.device.isConnected = true;
      $rootScope.$digest();

      expect($(element).find('.widget widget-header > .header widget-disconnected-icon').length).toEqual(0);
    });

  });

  describe('the currentAction controls', function () {

    it('exists when device.currentAction exists', function () {
      $rootScope.device.currentAction = 'derp';
      $rootScope.$digest();

      expect(selectControls().length).toEqual(1);
    });

    it('does not exist when device.currentAction does not exist', function () {
      delete $rootScope.device.currentAction;
      $rootScope.$digest();

      expect(selectControls().length).toEqual(0);
    });

    it('is bound to device.currentAction', function () {
      $rootScope.device.currentAction = 'derp';
      $rootScope.$digest();

      expect(selectControls().text().indexOf('derp') >= 0).toEqual(true);

      $rootScope.device.currentAction = 'herp';
      $rootScope.$digest();

      expect(selectControls().text().indexOf('derp') < 0).toEqual(true);
      expect(selectControls().text().indexOf('herp') >= 0).toEqual(true);
    });


    function selectControls() {
      return $(element).find('.widget current-action-controls');
    }
  });

  describe('the binary sensor controls', function () {

    it('exists when device.binarySensor.timeStamp exists', function () {
      $rootScope.device.binarySensor = {
        timeStamp: {}
      };
      $rootScope.$digest();

      expect(selectControls().length).toEqual(1);
    });

    it('does not exist when device.binarySensor.timeStamp does not exist', function () {
      $rootScope.device.binarySensor = {};
      $rootScope.$digest();

      expect(selectControls().length).toEqual(0);
    });

    it('is bound to device.binarySensor', function () {
      $rootScope.device.binarySensor = {
        value: true,
        timeStamp: {}
      };
      $rootScope.$digest();

      expect(selectControls().text().indexOf('True') >= 0).toEqual(true);
      expect(selectControls().text().indexOf('False') < 0).toEqual(true);

      $rootScope.device.binarySensor.value = false;
      $rootScope.$digest();

      expect(selectControls().text().indexOf('True') < 0).toEqual(true);
      expect(selectControls().text().indexOf('False') >= 0).toEqual(true);
    });


    function selectControls() {
      return $(element).find('.widget binary-sensor-controls');
    }
  });

  describe('the temperature sensor controls', function() {

    it('exists when device.temperatureSensor.value exists and device.hasThermostat() returns false', function () {
      $rootScope.device.temperatureSensor = {
        value: {}
      };
      $rootScope.hasThermostat = function() {
        return false;
      };
      $rootScope.$digest();

      expect(selectControls().length).toEqual(1);
    });

    it('does not exist when device.temperatureSensor.value exists and device.hasThermostat() returns true', function () {
      $rootScope.device.temperatureSensor = {
        value: {}
      };
      $rootScope.device.hasThermostat = function () {
        return true;
      };
      $rootScope.$digest();

      expect(selectControls().length).toEqual(0);
    });

    it('does not exist when device.temperatureSensor.value does not exist and device.hasThermostat() returns true', function () {
      $rootScope.device.temperatureSensor = {};
      $rootScope.device.hasThermostat = function () {
        return true;
      };
      $rootScope.$digest();

      expect(selectControls().length).toEqual(0);
    });

    it('does not exist when device.temperatureSensor.value does not exist and device.hasThermostat() returns false', function () {
      $rootScope.device.temperatureSensor = {};
      $rootScope.device.hasThermostat = function () {
        return false;
      };
      $rootScope.$digest();

      expect(selectControls().length).toEqual(0);
    });

    it('is bound to device.temperatureSensor', function() {
      $rootScope.device.temperatureSensor = {
        value: {
          value: 10
        }
      };
      $rootScope.device.hasThermostat = function () {
        return false;
      };
      $rootScope.$digest();

      expect(selectControls().text().indexOf('10') >= 0).toEqual(true);
      expect(selectControls().text().indexOf('15') < 0).toEqual(true);

      $rootScope.device.temperatureSensor.value.value = 15;
      $rootScope.$digest();

      expect(selectControls().text().indexOf('10') < 0).toEqual(true);
      expect(selectControls().text().indexOf('15') >= 0).toEqual(true);
    });


    function selectControls() {
      return $(element).find('.widget multilevel-sensor-controls').filter(function() {
        return $(this).text().indexOf('Temperature') >= 0;
      });
    }
  });

  describe('the humidity sensor controls', function () {

    it('exists when device.temperatureSensor.value exists', function () {
      $rootScope.device.humiditySensor = {
        value: {}
      };
      $rootScope.$digest();

      expect(selectControls().length).toEqual(1);
    });

    it('does not exist when device.humiditySensor.value does not exist', function () {
      $rootScope.device.temperatureSensor = {};
      $rootScope.$digest();

      expect(selectControls().length).toEqual(0);
    });

    it('is bound to device.humiditySensor', function () {
      $rootScope.device.humiditySensor = {
        value: {
          value: 10
        }
      };
      $rootScope.$digest();

      expect(selectControls().text().indexOf('10') >= 0).toEqual(true);
      expect(selectControls().text().indexOf('15') < 0).toEqual(true);

      $rootScope.device.humiditySensor.value.value = 15;
      $rootScope.$digest();

      expect(selectControls().text().indexOf('10') < 0).toEqual(true);
      expect(selectControls().text().indexOf('15') >= 0).toEqual(true);
    });


    function selectControls() {
      return $(element).find('.widget multilevel-sensor-controls').filter(function () {
        return $(this).text().indexOf('Humidity') >= 0;
      });
    }
  });

  describe('the illuminance sensor controls', function () {

    it('exists when device.illuminanceSensor.value exists', function () {
      $rootScope.device.illuminanceSensor = {
        value: {}
      };
      $rootScope.$digest();

      expect(selectControls().length).toEqual(1);
    });

    it('does not exist when device.illuminanceSensor.value does not exist', function () {
      $rootScope.device.illuminanceSensor = {};
      $rootScope.$digest();

      expect(selectControls().length).toEqual(0);
    });

    it('is bound to device.illuminanceSensor', function () {
      $rootScope.device.illuminanceSensor = {
        value: {
          value: 10
        }
      };
      $rootScope.$digest();

      expect(selectControls().text().indexOf('10') >= 0).toEqual(true);
      expect(selectControls().text().indexOf('15') < 0).toEqual(true);

      $rootScope.device.illuminanceSensor.value.value = 15;
      $rootScope.$digest();

      expect(selectControls().text().indexOf('10') < 0).toEqual(true);
      expect(selectControls().text().indexOf('15') >= 0).toEqual(true);
    });


    function selectControls() {
      return $(element).find('.widget multilevel-sensor-controls').filter(function () {
        return $(this).text().indexOf('Illuminance') >= 0;
      });
    }
  });

  describe('the power sensor controls', function () {

    it('exists when device.powerSensor.value exists', function () {
      $rootScope.device.powerSensor = {
        value: {}
      };
      $rootScope.$digest();

      expect(selectControls().length).toEqual(1);
    });

    it('does not exist when device.powerSensor.value does not exist', function () {
      $rootScope.device.powerSensor = {};
      $rootScope.$digest();

      expect(selectControls().length).toEqual(0);
    });

    it('is bound to device.powerSensor', function () {
      $rootScope.device.powerSensor = {
        value: {
          value: 10
        }
      };
      $rootScope.$digest();

      expect(selectControls().text().indexOf('10') >= 0).toEqual(true);
      expect(selectControls().text().indexOf('15') < 0).toEqual(true);

      $rootScope.device.powerSensor.value.value = 15;
      $rootScope.$digest();

      expect(selectControls().text().indexOf('10') < 0).toEqual(true);
      expect(selectControls().text().indexOf('15') >= 0).toEqual(true);
    });


    function selectControls() {
      return $(element).find('.widget multilevel-sensor-controls').filter(function () {
        return $(this).text().indexOf('Power') >= 0;
      });
    }
  });

  describe('the binary switch controls', function() {

    it('exists when device.binarySwitch.power exists', function() {
      $rootScope.device.binarySwitch = {
        power: "any value. not picky about value at this level of abstraction"
      };
      $rootScope.$digest();

      expect(selectControls().length).toEqual(1);
    });

    it('does now exist when device.binarySwitch.power is undefined', function() {
      $rootScope.device.binarySwitch = {};
      $rootScope.$digest();

      expect(selectControls().length).toEqual(0);
    });

    it('is bound to device.binarySwitch', function() {
      $rootScope.device.binarySwitch = {
        power: "a value that should result in no activated buttons"
      };
      $rootScope.$digest();

      expect(selectControls().find('.button.activated')).length = 0;

      $rootScope.device.binarySwitch.power = "on";
      $rootScope.$digest();

      expect(selectControls().find('.button.activated')).length = 1;
    });

    function selectControls() {
      return $(element).find('.widget binary-switch-controls');
    }
  });

  describe('the multilevel switch controls', function() {

    it('exists when device.multilevelSwitch.power exists', function() {
      $rootScope.device.multilevelSwitch = {
        power: 'any value. not picky about value at this level of abstraction'
      };
      $rootScope.$digest();

      expect(selectControls().length).toEqual(1);
    });

    it('exists when device.multilevelSwitch.power = 0 (special case)', function() {
      $rootScope.device.multilevelSwitch = {
        power: 0
      };
      $rootScope.$digest();

      expect(selectControls().length).toEqual(1);
    });

    it('does now exist when device.multilevelSwitch.power is undefined', function() {
      $rootScope.device.multilevelSwitch = {};
      $rootScope.$digest();

      expect(selectControls().length).toEqual(0);
    });

    it('is bound to device.multilevelSwitch', function() {
      $rootScope.device.multilevelSwitch = {
        power: -1
      };
      $rootScope.$digest();

      expect(selectControls().find('.button.activated')).length = 0;

      $rootScope.device.multilevelSwitch.power = 1;
      $rootScope.$digest();

      expect(selectControls().find('.button.activated')).length = 1;
    });

    function selectControls() {
      return $(element).find('.widget multilevel-switch-controls');
    }
  });

  describe('the color switch controls', function () {

    it('exists when device.colorSwitch.value exists', function () {
      $rootScope.device.colorSwitch = {
        value: {}
      };
      $rootScope.$digest();

      expect(selectControls().length).toEqual(1);
    });

    it('does now exist when device.colorSwitch.value is undefined', function () {
      $rootScope.device.colorSwitch = {};
      $rootScope.$digest();

      expect(selectControls().length).toEqual(0);
    });

    function selectControls() {
      return $(element).find('.widget color-switch-controls');
    }
  });

  describe('the thermostat controls', function() {

    it('exists when device.hasThermostat() returns true', function() {
      $rootScope.device.hasThermostat = function() {
        return true;
      };
      $rootScope.$digest();

      expect(selectControls().length).toEqual(1);
    });

    it('does not exist when device.hasThermostat() returns false', function() {
      $rootScope.device.hasThermostat = function() {
        return false;
      };
      $rootScope.$digest();

      expect(selectControls().length).toEqual(0);
    });

    it('is bound to device.thermostat', function() {
      $rootScope.device.hasThermostat = function() {
        return true;
      };
      $rootScope.device.thermostat = {
        core: {
          currentAction: 'derping'
        }
      };
      $rootScope.$digest();

      expect($(element).text()).toContain('Derping');
      expect($(element).text()).not.toContain('Herping');

      $rootScope.device.thermostat = {
        core: {
          currentAction: 'herping'
        }
      };
      $rootScope.$digest();

      expect($(element).text()).not.toContain('Derping');
      expect($(element).text()).toContain('Herping');

    });

    it('is bound to device.temperatureSensor', function() {
      $rootScope.device.hasThermostat = function() {
        return true;
      };
      $rootScope.device.temperatureSensor = {
        value: {
          value: 10
        }
      };
      $rootScope.$digest();

      expect($(element).text()).toContain('10');
      expect($(element).text()).not.toContain('20');

      $rootScope.device.temperatureSensor = {
        value: {
          value: 20
        }
      };
      $rootScope.$digest();

      expect($(element).text()).not.toContain('10');
      expect($(element).text()).toContain('20');
    });

    function selectControls() {
      return $(element).find('.widget thermostat-controls');
    }
  });

});
