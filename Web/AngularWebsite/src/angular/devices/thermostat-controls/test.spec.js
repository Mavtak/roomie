describe('angular roomie.devices thermostat-controls (directive)', function () {
  var $compile;
  var $rootScope;
  var attributes;
  var element;

  beforeEach(angular.mock.module('roomie.devices'));

  beforeEach(angular.mock.inject(function ($injector) {
    $compile = $injector.get('$compile');
    $rootScope = $injector.get('$rootScope');
  }));

  beforeEach(function () {
    element = $compile('<thermostat-controls temperature-sensor="attributes.temperatureSensor" thermostat="attributes.thermostat"></thermostat-controls>')($rootScope);

    attributes = {
      thermostat: {
        core: {},
        fan: {}
      }
    };

    $rootScope.attributes = attributes;
    $rootScope.$digest();
  });

  describe('the thermostat-temperature-controls element', function () {

    describe('existance criteria', function () {

      it('the "thermostat" attribute has a "setpoints.cool" property', function () {
        attributes.thermostat = {
          setpoints: {
            cool: {}
          }
        };
        $rootScope.$digest();

        expect($(element).find('thermostat-temperature-controls').length).toEqual(1);
      });

      it('the "thermostat" attribute has a "setpoints.heat" property', function () {
        attributes.thermostat = {
          setpoints: {
            heat: {}
          }
        };
        $rootScope.$digest();

        expect($(element).find('thermostat-temperature-controls').length).toEqual(1);
      });

      it('the "temperature-sensor" attribute has a "value" property', function () {
        attributes.temperatureSensor = {
          value: {}
        };
        $rootScope.$digest();

        expect($(element).find('thermostat-temperature-controls').length).toEqual(1);
      });

    });

    describe('nonexistance criteria', function () {

      it('none of the existance criteria are met', function () {
        expect($(element).find('thermostat-temperature-controls').length).toEqual(0);
      });

    });

  });

  describe('the thermostat-mode-controls element for the system mode', function () {

    describe('existance criteria', function () {

      it('the "thermostat" attribute has a "core.currentAction" property', function () {
        attributes.thermostat.core.currentAction = 'derp';
        $rootScope.$digest();

        expect(selectModeControls('System Mode').length).toEqual(1);
      });

      it('the "thermostat" attribute has a "core.mode" property', function () {
        attributes.thermostat.core.mode = 'derping';
        $rootScope.$digest();

        expect(selectModeControls('System Mode').length).toEqual(1);
      });

      it('the "thermostat" attribute has a "core.supportedModes" property that is an array with more than one item', function () {
        attributes.thermostat.core.supportedModes = ['derp'];
        $rootScope.$digest();

        expect(selectModeControls('System Mode').length).toEqual(1);
      });

    });

    describe('nonexistance criteria', function () {

      it('none of the existance criteria are met', function () {
        expect(selectModeControls('System Mode').length).toEqual(0);
      });

      it('none of the existance criteria are met, and the "thermostat" attribute has a "core.supportedModes" property is an empty array', function () {
        attributes.thermostat.core.supportedModes = [];
        $rootScope.$digest();

        expect(selectModeControls('System Mode').length).toEqual(0);
      });

    });

  });

  describe('the thermostat-mode-controls element for the fan mode', function () {

    describe('existance criteria', function () {

      it('the "thermostat" attribute has a "fan.currentAction" property', function () {
        attributes.thermostat.fan.currentAction = 'derp';
        $rootScope.$digest();

        expect(selectModeControls('Fan Mode').length).toEqual(1);
      });

      it('the "thermostat" attribute has a "fan.mode" property', function () {
        attributes.thermostat.fan.mode = 'derping';
        $rootScope.$digest();

        expect(selectModeControls('Fan Mode').length).toEqual(1);
      });

      it('the "thermostat" attribute has a "fan.supportedModes" property that is an array with more than one item', function () {
        attributes.thermostat.fan.supportedModes = ['derp'];
        $rootScope.$digest();

        expect(selectModeControls('Fan Mode').length).toEqual(1);
      });

    });

    describe('nonexistance criteria', function () {

      it('none of the existance criteria are met', function () {
        expect(selectModeControls('System Mode').length).toEqual(0);
      });

      it('none of the existance criteria are met, and the "thermostat" attribute has a "fan.supportedModes" property is an empty array', function () {
        attributes.thermostat.fan.supportedModes = [];
        $rootScope.$digest();

        expect(selectModeControls('Fan Mode').length).toEqual(0);
      });

    });

  });

  function selectModeControls(name) {
    return $(element).find('thermostat-mode-controls').filter(function () {
      return $(this).find('.header').text().indexOf(name) >= 0;
    });
  }

});
