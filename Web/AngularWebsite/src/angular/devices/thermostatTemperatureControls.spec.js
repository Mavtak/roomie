/// <reference path="../../../Scripts/Libraries/jquery-1.5.1.min.js"/>
/// <reference path="../../../Scripts/Libraries/angular-1.3.13.min.js"/>
/// <reference path="../angular-mocks.js"/>
/// <reference path="../../../Scripts/angular/dependencies.js"/>
/// <reference path="../../../Scripts/angular/common/widgetButton.js"/>
/// <reference path="../../../Scripts/angular/devices/thermostatSingleTemperatureControls.js"/>
/// <reference path="../../../Scripts/angular/devices/thermostatTemperatureControls.js"/>

describe('roomie.devices.thermostatTemperatureControls', function() {
  var $compile;
  var $rootScope;
  var attributes;
  var element;

  beforeEach(angular.mock.module('roomie.devices'));

  beforeEach(angular.mock.inject(function($injector) {
    $compile = $injector.get('$compile');
    $rootScope = $injector.get('$rootScope');
  }));

  beforeEach(function() {
    element = $compile('<thermostat-temperature-controls setpoints="attributes.setpoints" temperature="attributes.temperature" ></thermostat-temperature-controls>')($rootScope);

    attributes = {
      setpoints: {
        cool: {
          value: 50,
          units: 'McDerps'
        },
        heat: {
          value: 25,
          units: 'McDerps'
        },
        set: jasmine.createSpy()
      },
      temperature: {
        value: 30,
        units: 'McDerps'
      }
    };

    $rootScope.attributes = attributes;
    $rootScope.$digest();
  });

  describe('the temperature displays', function() {

    it('has 3', function() {
      var displays = selectTemperatureDisplays();

      expect(displays.length).toEqual(3);
    });

    describe('the first one', function() {

      it('is the heat setpoint', function() {
        var display = selectTemperatureDisplay(0);

        expect(display.find('.value').text()).toContain('25');
        expect(display.find('.description').text()).toContain('Heat');
      });

      describe('the cooler button', function() {

        it('sets the heat setpoint to cooler', function() {
          var button = selectSetpointButton(0, '-');

          expect(attributes.setpoints.set).not.toHaveBeenCalled();

          button.click();

          expect(attributes.setpoints.set).toHaveBeenCalled();

          var args = attributes.setpoints.set.calls.mostRecent().args;

          expect(args[0]).toEqual('heat');
          expect(args[1]).toEqual({
            value: 24,
            units: 'McDerps'
          });
        });

      });

      describe('the hotter button', function() {

        it('sets the heat setpoint to hotter', function() {
          var button = selectSetpointButton(0, '+');

          expect(attributes.setpoints.set).not.toHaveBeenCalled();

          button.click();

          expect(attributes.setpoints.set).toHaveBeenCalled();

          var args = attributes.setpoints.set.calls.mostRecent().args;

          expect(args[0]).toEqual('heat');
          expect(args[1]).toEqual({
            value: 26,
            units: 'McDerps'
          });
        });

      });

    });

    describe('the second one', function() {

      it('is the current temperature', function() {
        var display = selectTemperatureDisplay(1);

        expect(display.find('.value').text()).toContain('30');
        expect(display.find('.description').text()).toContain('Current');
      });

      describe('the cooler button', function() {

        it('does not exist', function() {
          var button = selectSetpointButton(1, '-');

          expect(button.length).toEqual(0);
        });

      });

      describe('the hotter button', function() {

        it('does not exist', function() {
          var button = selectSetpointButton(1, '+');

          expect(button.length).toEqual(0);
        });

      });

    });

    describe('the third one', function() {

      it('is the cool setpoint', function() {
        var display = selectTemperatureDisplay(2);

        expect(display.find('.value').text()).toContain('50');
        expect(display.find('.description').text()).toContain('Cool');
      });

      describe('the cooler button', function() {

        it('sets the heat setpoint to cooler', function() {
          var button = selectSetpointButton(2, '-');

          expect(attributes.setpoints.set).not.toHaveBeenCalled();

          button.click();

          expect(attributes.setpoints.set).toHaveBeenCalled();

          var args = attributes.setpoints.set.calls.mostRecent().args;

          expect(args[0]).toEqual('cool');
          expect(args[1]).toEqual({
            value: 49,
            units: 'McDerps'
          });
        });

      });

      describe('the hotter button', function() {

        it('sets the heat setpoint to hotter', function() {
          var button = selectSetpointButton(2, '+');

          expect(attributes.setpoints.set).not.toHaveBeenCalled();

          button.click();

          expect(attributes.setpoints.set).toHaveBeenCalled();

          var args = attributes.setpoints.set.calls.mostRecent().args;

          expect(args[0]).toEqual('cool');
          expect(args[1]).toEqual({
            value: 51,
            units: 'McDerps'
          });
        });

      });

    });

  });

  function selectTemperatureDisplays() {
    return $(element).find('.thermostat-controls thermostat-single-temperature-controls');
  }

  function selectTemperatureDisplay(index) {
    return selectTemperatureDisplays().eq(index);
  }

  function selectSetpointButton(index, label) {
    return selectTemperatureDisplay(index).find('.setpoint-button').filter(function() {
      return $(this).text() === label;
    });
  }
});
