/// <reference path="../../../Scripts/Libraries/jquery-1.5.1.min.js"/>
/// <reference path="../../../Scripts/Libraries/angular-1.3.13.min.js"/>
/// <reference path="../angular-mocks.js"/>
/// <reference path="../../../Scripts/angular/dependencies.js"/>
/// <reference path="../../../Scripts/angular/common/widget.js"/>
/// <reference path="../../../Scripts/angular/common/widgetButton.js"/>
/// <reference path="../../../Scripts/angular/common/widgetButtonGroup.js"/>
/// <reference path="../../../Scripts/angular/devices/binarySwitchControls.js"/>

describe('roomie.devices.binarySwitchControls', function() {
  var $compile;
  var $rootScope;
  var element;

  beforeEach(angular.mock.module('roomie.devices'));

  beforeEach(angular.mock.inject(function ($injector) {
    $compile = $injector.get('$compile');
    $rootScope = $injector.get('$rootScope');
  }));

  beforeEach(function() {
    element = $compile('<binary-switch-controls binary-switch="binarySwitch"></binary-switch-controls>')($rootScope);

    $rootScope.binarySwitch = {};
  });

  describe('the button group', function() {

    it('has one', function() {
      $rootScope.$digest();

      expect($(element).find('.buttonGroup').length).toEqual(1);
    });

    it('has two buttons', function() {
      $rootScope.$digest();

      expect(selectButtons().length).toEqual(2);
    });

    describe('the off button', function() {

      it('is the first button (identified by the label)', function() {
        $rootScope.$digest();

        expect(selectButton(0).html()).toEqual('Off');
      });

      describe('activation styling', function() {

        it('yes when binarySwitch.power is off', function() {
          $rootScope.binarySwitch.power = "off";
          $rootScope.$digest();

          expect(selectButton(0).hasClass('activated')).toEqual(true);
        });

        it('no when binarySwitch.power is on', function() {
          $rootScope.binarySwitch.power = "on";
          $rootScope.$digest();

          expect(selectButton(0).hasClass('activated')).toEqual(false);
        });

        it('no when binarySwitch.power is Off (upper case)', function() {
          $rootScope.binarySwitch.power = "Off";
          $rootScope.$digest();

          expect(selectButton(0).hasClass('activated')).toEqual(false);
        });

        it('no when binarySwitch.power is something unexpected', function() {
          $rootScope.binarySwitch.power = "blam";
          $rootScope.$digest();

          expect(selectButton(0).hasClass('activated')).toEqual(false);
        });

        it('no when binarySwitch.power is undefined', function() {
          $rootScope.$digest();

          expect(selectButton(1).hasClass('activated')).toEqual(false);
        });

      });

      describe('clicking', function() {

        it('calls binarySwitch.setPower("On") when clicked', function() {
          $rootScope.binarySwitch.setPower = jasmine.createSpy('setPower');
          $rootScope.$digest();

          expect($rootScope.binarySwitch.setPower.calls.count()).toEqual(0);

          selectButton(0).click();

          expect($rootScope.binarySwitch.setPower.calls.count()).toEqual(1);
        });

        it('if fine if binarySwitch.setPower does not exist when clicked', function() {
          $rootScope.$digest();

          selectButton(0).click();
        });

      });

    });

    describe('the on button', function() {

      it('is the second button (identified by the label)', function() {
        $rootScope.$digest();

        expect(selectButton(1).html()).toEqual('On');
      });

      describe('activation styling', function() {

        it('yes when binarySwitch.power is on', function() {
          $rootScope.binarySwitch.power = "on";
          $rootScope.$digest();

          expect(selectButton(1).hasClass('activated')).toEqual(true);
        });

        it('no when binarySwitch.power is off', function() {
          $rootScope.binarySwitch.power = "off";
          $rootScope.$digest();

          expect(selectButton(1).hasClass('activated')).toEqual(false);
        });

        it('no when binarySwitch.power is On (upper case)', function() {
          $rootScope.binarySwitch.power = "On";
          $rootScope.$digest();

          expect(selectButton(1).hasClass('activated')).toEqual(false);
        });

        it('no when binarySwitch.power is something unexpected', function() {
          $rootScope.binarySwitch.power = "blam";
          $rootScope.$digest();

          expect(selectButton(1).hasClass('activated')).toEqual(false);
        });

        it('no when binarySwitch.power is undefined', function() {
          $rootScope.$digest();

          expect(selectButton(1).hasClass('activated')).toEqual(false);
        });

      });

      describe('clicking', function() {

        it('calls binarySwitch.setPower("On") when clicked', function() {
          $rootScope.binarySwitch.setPower = jasmine.createSpy('setPower');
          $rootScope.$digest();

          expect($rootScope.binarySwitch.setPower.calls.count()).toEqual(0);

          selectButton(1).click();

          expect($rootScope.binarySwitch.setPower.calls.count()).toEqual(1);
        });

        it('if fine if binarySwitch.setPower does not exist when clicked', function() {
          $rootScope.$digest();

          selectButton(1).click();
        });

      });

    });

    function selectButtons() {
      return $(element).find('.buttonGroup .button').filter(':not(.button .button)');
    }

    function selectButton(index) {
      return selectButtons().eq(index).find('.button');
    }
  });

});
