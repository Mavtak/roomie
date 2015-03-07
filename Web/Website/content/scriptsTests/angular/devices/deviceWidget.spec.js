/// <reference path="../../../Scripts/Libraries/jquery-1.5.1.min.js"/>
/// <reference path="../../../Scripts/Libraries/angular-1.3.13.min.js"/>
/// <reference path="../angular-mocks.js"/>
/// <reference path="../../../Scripts/angular/dependencies.js"/>
/// <reference path="../../../Scripts/angular/common/widget.js"/>
/// <reference path="../../../Scripts/angular/common/widgetHeader.js"/>
/// <reference path="../../../Scripts/angular/devices/deviceWidget.js"/>

describe('roomie.devices.deviceWidget', function() {
  var $compile;
  var $rootScope;
  var element;

  beforeEach(angular.mock.module('roomie.devices'));

  beforeEach(angular.mock.inject(function(_$compile_, _$rootScope_) {
    $compile = _$compile_;
    $rootScope = _$rootScope_;
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

      it('does not link to anywhere', function() {
        $rootScope.$digest();

        expect($(element).find('.widget widget-header .header').attr('href')).not.toBeDefined();
      });

      it('has no subtitle', function() {
        $rootScope.$digest();

        expect($(element).find('.widget widget-header .header .location').html()).toEqual('');
      });

    });

  });

});