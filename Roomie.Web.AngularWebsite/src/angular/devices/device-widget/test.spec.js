describe('angular roomie.devices device-widget (directive)', function () {
  var $injector;
  var $scope;

  beforeEach(angular.mock.module('roomie.devices'));

  beforeEach(angular.mock.inject(function (_$injector_) {
    $injector = _$injector_;
    $scope = $injector.get('$rootScope').$new();
  }));

  beforeEach(function () {
    $scope.device = {};
  });

  describe('the header', function () {
    var element;

    beforeEach(function () {
      element = compileDirective('<device-widget device="device"></device-widget>');
    });

    it('has one', function () {
      expect($(element).find('.widget widget-header .header').length).toEqual(1);
    });

    it('has a title that matches the device name', function () {
      $scope.device.name = "Light Switch";
      $scope.$digest();

      expect($(element).find('.widget widget-header .header .name').html().trim()).toEqual("Light Switch");
    });

    it('links to the detail', function () {
      $scope.device.id = '123';
      $scope.$digest();

      expect($(element).find('.widget widget-header .header').attr('href')).toEqual('#/devices/123');
    });

    describe('the subtitle', function () {

      it('usually has none', function () {
        expect($(element).find('.widget widget-header .header .location').html().trim()).toEqual('');
      });

      it('has one for thermostats', function () {
        $scope.device.thermostat = {
          core: {
            currentAction: 'derping'
          }
        };

        $scope.$digest();

        expect($(element).find('.widget widget-header .header .location').html().trim()).toEqual('Currently Derping');
      });

    });

    it('sets the disconnected property', function () {
      $scope.device.isConnected = false;
      $scope.$digest();

      expect($(element).find('.widget widget-header > .header widget-disconnected-icon').length).toEqual(1);

      $scope.device.isConnected = true;
      $scope.$digest();

      expect($(element).find('.widget widget-header > .header widget-disconnected-icon').length).toEqual(0);
    });

  });

  describe('the currentAction controls', function () {
    var element;

    beforeEach(function () {
      element = compileDirective('<device-widget device="device"></device-widget>');
    });

    it('exists when device.currentAction exists', function () {
      $scope.device.currentAction = 'derp';
      $scope.$digest();

      expect(selectControls().length).toEqual(1);
    });

    it('does not exist when device.currentAction does not exist', function () {
      delete $scope.device.currentAction;
      $scope.$digest();

      expect(selectControls().length).toEqual(0);
    });

    it('is bound to device.currentAction', function () {
      $scope.device.currentAction = 'derp';
      $scope.$digest();

      expect(selectControls().text().indexOf('derp') >= 0).toEqual(true);

      $scope.device.currentAction = 'herp';
      $scope.$digest();

      expect(selectControls().text().indexOf('derp') < 0).toEqual(true);
      expect(selectControls().text().indexOf('herp') >= 0).toEqual(true);
    });


    function selectControls() {
      return $(element).find('.widget current-action-controls');
    }

  });

  describe('the binary sensor controls', function () {
    var element;

    beforeEach(function () {
      element = compileDirective('<device-widget device="device"></device-widget>');
    });

    it('exists when device.binarySensor.timeStamp exists', function () {
      $scope.device.binarySensor = {
        timeStamp: {}
      };
      $scope.$digest();

      expect(selectControls().length).toEqual(1);
    });

    it('does not exist when device.binarySensor.timeStamp does not exist', function () {
      $scope.device.binarySensor = {};
      $scope.$digest();

      expect(selectControls().length).toEqual(0);
    });

    it('is bound to device.binarySensor', function () {
      $scope.device.binarySensor = {
        value: true,
        timeStamp: {}
      };
      $scope.$digest();

      expect(selectControls().text().indexOf('True') >= 0).toEqual(true);
      expect(selectControls().text().indexOf('False') < 0).toEqual(true);

      $scope.device.binarySensor.value = false;
      $scope.$digest();

      expect(selectControls().text().indexOf('True') < 0).toEqual(true);
      expect(selectControls().text().indexOf('False') >= 0).toEqual(true);
    });


    function selectControls() {
      return $(element).find('.widget binary-sensor-controls');
    }

  });

  describe('the temperature sensor controls', function () {
    var element;

    beforeEach(function () {
      element = compileDirective('<device-widget device="device"></device-widget>');
    });

    it('exists when device.temperatureSensor.value exists and device.hasThermostat() returns false', function () {
      $scope.device.temperatureSensor = {
        value: {}
      };
      $scope.hasThermostat = function () {
        return false;
      };
      $scope.$digest();

      expect(selectControls().length).toEqual(1);
    });

    it('does not exist when device.temperatureSensor.value exists and device.hasThermostat() returns true', function () {
      $scope.device.temperatureSensor = {
        value: {}
      };
      $scope.device.hasThermostat = function () {
        return true;
      };
      $scope.$digest();

      expect(selectControls().length).toEqual(0);
    });

    it('does not exist when device.temperatureSensor.value does not exist and device.hasThermostat() returns true', function () {
      $scope.device.temperatureSensor = {};
      $scope.device.hasThermostat = function () {
        return true;
      };
      $scope.$digest();

      expect(selectControls().length).toEqual(0);
    });

    it('does not exist when device.temperatureSensor.value does not exist and device.hasThermostat() returns false', function () {
      $scope.device.temperatureSensor = {};
      $scope.device.hasThermostat = function () {
        return false;
      };
      $scope.$digest();

      expect(selectControls().length).toEqual(0);
    });

    it('is bound to device.temperatureSensor', function () {
      $scope.device.temperatureSensor = {
        value: {
          value: 10
        }
      };
      $scope.device.hasThermostat = function () {
        return false;
      };
      $scope.$digest();

      expect(selectControls().text().indexOf('10') >= 0).toEqual(true);
      expect(selectControls().text().indexOf('15') < 0).toEqual(true);

      $scope.device.temperatureSensor.value.value = 15;
      $scope.$digest();

      expect(selectControls().text().indexOf('10') < 0).toEqual(true);
      expect(selectControls().text().indexOf('15') >= 0).toEqual(true);
    });


    function selectControls() {
      return $(element).find('.widget multilevel-sensor-controls').filter(function () {
        return $(this).text().indexOf('Temperature') >= 0;
      });
    }

  });

  describe('the humidity sensor controls', function () {
    var element;

    beforeEach(function () {
      element = compileDirective('<device-widget device="device"></device-widget>');
    });

    it('exists when device.temperatureSensor.value exists', function () {
      $scope.device.humiditySensor = {
        value: {}
      };
      $scope.$digest();

      expect(selectControls().length).toEqual(1);
    });

    it('does not exist when device.humiditySensor.value does not exist', function () {
      $scope.device.temperatureSensor = {};
      $scope.$digest();

      expect(selectControls().length).toEqual(0);
    });

    it('is bound to device.humiditySensor', function () {
      $scope.device.humiditySensor = {
        value: {
          value: 10
        }
      };
      $scope.$digest();

      expect(selectControls().text().indexOf('10') >= 0).toEqual(true);
      expect(selectControls().text().indexOf('15') < 0).toEqual(true);

      $scope.device.humiditySensor.value.value = 15;
      $scope.$digest();

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
    var element;

    beforeEach(function () {
      element = compileDirective('<device-widget device="device"></device-widget>');
    });

    it('exists when device.illuminanceSensor.value exists', function () {
      $scope.device.illuminanceSensor = {
        value: {}
      };
      $scope.$digest();

      expect(selectControls().length).toEqual(1);
    });

    it('does not exist when device.illuminanceSensor.value does not exist', function () {
      $scope.device.illuminanceSensor = {};
      $scope.$digest();

      expect(selectControls().length).toEqual(0);
    });

    it('is bound to device.illuminanceSensor', function () {
      $scope.device.illuminanceSensor = {
        value: {
          value: 10
        }
      };
      $scope.$digest();

      expect(selectControls().text().indexOf('10') >= 0).toEqual(true);
      expect(selectControls().text().indexOf('15') < 0).toEqual(true);

      $scope.device.illuminanceSensor.value.value = 15;
      $scope.$digest();

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
    var element;

    beforeEach(function () {
      element = compileDirective('<device-widget device="device"></device-widget>');
    });

    it('exists when device.powerSensor.value exists', function () {
      $scope.device.powerSensor = {
        value: {}
      };
      $scope.$digest();

      expect(selectControls().length).toEqual(1);
    });

    it('does not exist when device.powerSensor.value does not exist', function () {
      $scope.device.powerSensor = {};
      $scope.$digest();

      expect(selectControls().length).toEqual(0);
    });

    it('is bound to device.powerSensor', function () {
      $scope.device.powerSensor = {
        value: {
          value: 10
        }
      };
      $scope.$digest();

      expect(selectControls().text().indexOf('10') >= 0).toEqual(true);
      expect(selectControls().text().indexOf('15') < 0).toEqual(true);

      $scope.device.powerSensor.value.value = 15;
      $scope.$digest();

      expect(selectControls().text().indexOf('10') < 0).toEqual(true);
      expect(selectControls().text().indexOf('15') >= 0).toEqual(true);
    });


    function selectControls() {
      return $(element).find('.widget multilevel-sensor-controls').filter(function () {
        return $(this).text().indexOf('Power') >= 0;
      });
    }

  });

  describe('the binary switch controls', function () {
    var element;

    beforeEach(function () {
      element = compileDirective('<device-widget device="device"></device-widget>');
    });

    it('exists when device.binarySwitch.power exists', function () {
      $scope.device.binarySwitch = {
        power: "any value. not picky about value at this level of abstraction"
      };
      $scope.$digest();

      expect(selectControls().length).toEqual(1);
    });

    it('does now exist when device.binarySwitch.power is undefined', function () {
      $scope.device.binarySwitch = {};
      $scope.$digest();

      expect(selectControls().length).toEqual(0);
    });

    it('is bound to device.binarySwitch', function () {
      $scope.device.binarySwitch = {
        power: "a value that should result in no activated buttons"
      };
      $scope.$digest();

      expect(selectControls().find('.button.activated')).length = 0;

      $scope.device.binarySwitch.power = "on";
      $scope.$digest();

      expect(selectControls().find('.button.activated')).length = 1;
    });

    function selectControls() {
      return $(element).find('.widget binary-switch-controls');
    }

  });

  describe('the multilevel switch controls', function () {
    var element;

    beforeEach(function () {
      element = compileDirective('<device-widget device="device"></device-widget>');
    });

    it('exists when device.multilevelSwitch.power exists', function () {
      $scope.device.multilevelSwitch = {
        power: 'any value. not picky about value at this level of abstraction'
      };
      $scope.$digest();

      expect(selectControls().length).toEqual(1);
    });

    it('exists when device.multilevelSwitch.power = 0 (special case)', function () {
      $scope.device.multilevelSwitch = {
        power: 0
      };
      $scope.$digest();

      expect(selectControls().length).toEqual(1);
    });

    it('does now exist when device.multilevelSwitch.power is undefined', function () {
      $scope.device.multilevelSwitch = {};
      $scope.$digest();

      expect(selectControls().length).toEqual(0);
    });

    it('is bound to device.multilevelSwitch', function () {
      $scope.device.multilevelSwitch = {
        power: -1
      };
      $scope.$digest();

      expect(selectControls().find('.button.activated')).length = 0;

      $scope.device.multilevelSwitch.power = 1;
      $scope.$digest();

      expect(selectControls().find('.button.activated')).length = 1;
    });

    function selectControls() {
      return $(element).find('.widget multilevel-switch-controls');
    }

  });

  describe('the color switch controls', function () {
    var element;

    beforeEach(function () {
      element = compileDirective('<device-widget device="device"></device-widget>');
    });

    it('exists when device.colorSwitch.value exists', function () {
      $scope.device.colorSwitch = {
        value: {}
      };
      $scope.$digest();

      expect(selectControls().length).toEqual(1);
    });

    it('does now exist when device.colorSwitch.value is undefined', function () {
      $scope.device.colorSwitch = {};
      $scope.$digest();

      expect(selectControls().length).toEqual(0);
    });

    function selectControls() {
      return $(element).find('.widget color-switch-controls');
    }
  });

  describe('the thermostat controls', function () {
    var element;

    beforeEach(function () {
      element = compileDirective('<device-widget device="device"></device-widget>');
    });

    it('exists when device.hasThermostat() returns true', function () {
      $scope.device.hasThermostat = function () {
        return true;
      };
      $scope.$digest();

      expect(selectControls().length).toEqual(1);
    });

    it('does not exist when device.hasThermostat() returns false', function () {
      $scope.device.hasThermostat = function () {
        return false;
      };
      $scope.$digest();

      expect(selectControls().length).toEqual(0);
    });

    it('is bound to device.thermostat', function () {
      $scope.device.hasThermostat = function () {
        return true;
      };
      $scope.device.thermostat = {
        core: {
          currentAction: 'derping'
        }
      };
      $scope.$digest();

      expect($(element).text()).toContain('Derping');
      expect($(element).text()).not.toContain('Herping');

      $scope.device.thermostat = {
        core: {
          currentAction: 'herping'
        }
      };
      $scope.$digest();

      expect($(element).text()).not.toContain('Derping');
      expect($(element).text()).toContain('Herping');

    });

    it('is bound to device.temperatureSensor', function () {
      $scope.device.hasThermostat = function () {
        return true;
      };
      $scope.device.temperatureSensor = {
        value: {
          value: 10
        }
      };
      $scope.$digest();

      expect($(element).text()).toContain('10');
      expect($(element).text()).not.toContain('20');

      $scope.device.temperatureSensor = {
        value: {
          value: 20
        }
      };
      $scope.$digest();

      expect($(element).text()).not.toContain('10');
      expect($(element).text()).toContain('20');
    });

    function selectControls() {
      return $(element).find('.widget thermostat-controls');
    }
  });

  describe('the edit controls', function () {
    var element;

    beforeEach(function () {
      element = compileDirective('<device-widget device="device" show-edit="showEdit"></device-widget>');
    });

    it('shows them when can-edit is set to true', function () {
      $scope.showEdit = true;
      $scope.$digest();

      expect(selectControls().length).toEqual(1);
    });

    it('hides them when can-edit is set to false', function () {
      $scope.showEdit = false;

      expect(selectControls().length).toEqual(0);
    });

    it('hides them when can-edit is not set', function () {
      element = compileDirective('<device-widget device="device"></device-widget>');

      expect(selectControls().length).toEqual(0);
    });

    it('displays the state of the device', function () {
      $scope.device.name = 'Lamp or Something';
      $scope.showEdit = true;
      $scope.$digest();

      var match = selectControls().find('*').filter(function () {
        return $(this).val() === 'Lamp or Something';
      });

      expect(match.length).toEqual(1);
    });

    function selectControls() {
      return $(element).find('.widget device-edit-controls');
    }

  });

  function compileDirective(html) {
    var $compile = $injector.get('$compile');
    var element = $compile(html)($scope);
    $scope.$digest();

    return element;
  }

});
