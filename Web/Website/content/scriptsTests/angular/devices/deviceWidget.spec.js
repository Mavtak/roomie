/// <reference path="../../../Scripts/Libraries/jquery-1.5.1.min.js"/>
/// <reference path="../../../Scripts/Libraries/angular-1.3.13.min.js"/>
/// <reference path="../angular-mocks.js"/>
/// <reference path="../../../Scripts/angular/dependencies.js"/>
/// <reference path="../../../Scripts/angular/common/widget.js"/>
/// <reference path="../../../Scripts/angular/common/widgetHeader.js"/>
/// <reference path="../../../Scripts/angular/devices/binarySwitchDeviceControls.js"/>
/// <reference path="../../../Scripts/angular/devices/deviceWidget.js"/>

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

  describe('the structure', function() {

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

    });

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

});