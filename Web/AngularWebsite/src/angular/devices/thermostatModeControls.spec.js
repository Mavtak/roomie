/// <reference path="../../../Scripts/Libraries/jquery-1.5.1.min.js"/>
/// <reference path="../../../Scripts/Libraries/angular-1.3.13.min.js"/>
/// <reference path="../angular-mocks.js"/>
/// <reference path="../../../Scripts/angular/dependencies.js"/>
/// <reference path="../../../Scripts/angular/common/widgetButton.js"/>
/// <reference path="../../../Scripts/angular/devices/thermostatModeControls.js"/>

describe('roomie.devices.thermostatModeControls', function() {
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
    element = $compile('<thermostat-mode-controls label="The Things" modes="attributes.modes"></thermostat-mode-controls>')($rootScope);

    attributes = {
      modes: {
        currentAction: 'derping',
        mode: 'derp',
        set: jasmine.createSpy(),
        supportedModes: [
          'herp',
          'derp',
          'berp'
        ]
      }
    };

    $rootScope.attributes = attributes;
    $rootScope.$digest();
  });

  describe('the header', function() {
    var header;

    beforeEach(function() {
      header = $(element).find('.header');
    });

    it('exists', function() {
      expect(header.length).toEqual(1);
    });

    describe('the primary text', function() {

      it('is the label attribute', function() {
        var text = header.clone()
          .children().remove().end()
          .text();

        expect(text).toEqual('The Things');
      });

    });

    describe('the secondary text', function() {

      it('is the formatted current action', function() {
        expect(header.find('.secondary').text()).toEqual('Currently Derping');
      });

      it('is nothing if the current action is not a string', function() {
        attributes.modes.currentAction = {};
        $rootScope.$digest();

        expect(header.find('.secondary').text()).toEqual('');
      });

    });

  });

  describe('the buttons', function() {

    describe('existence criteria', function() {

      it('the "modes" attribute has a "supportedModes" property that is an array with entries', function() {
        var buttonGroup = selectButtonGroup();

        expect(buttonGroup.length).toEqual(1);
      });

    });

    describe('nonexistence criteria', function() {

      it('the "modes" attribute has a "supportedModes" property that is an empty array', function() {
        attributes.modes.supportedModes = [];
        $rootScope.$digest();
        var buttonGroup = selectButtonGroup();

        expect(buttonGroup.length).toEqual(0);
      });

      it('the "modes" attribute does not have a "supportedModes" property', function() {
        delete attributes.modes.supportedModes;
        $rootScope.$digest();
        var buttonGroup = selectButtonGroup();

        expect(buttonGroup.length).toEqual(0);
      });

    });

    it('has as many as there are supported modes', function() {
      var buttons = selectButtons();

      expect(buttons.length).toEqual(3);
    });

    it('labels each button', function() {
      expect(selectButton(0).text()).toEqual('Herp');
      expect(selectButton(1).text()).toEqual('Derp');
      expect(selectButton(2).text()).toEqual('Berp');
    });

    it('sets activation based on the current mode', function() {
      expect(selectButton(0).hasClass('activated')).toEqual(false);
      expect(selectButton(1).hasClass('activated')).toEqual(true);
      expect(selectButton(2).hasClass('activated')).toEqual(false);
    });

    it('supports clicking', function() {
      var button = selectButton(2);

      expect(attributes.modes.set).not.toHaveBeenCalled();

      button.click();

      expect(attributes.modes.set).toHaveBeenCalledWith('berp');
    });

    function selectButtonGroup() {
      return $(element).find('widget-button-group');
    }

    function selectButtons() {
      return selectButtonGroup().find('.button .button');
    }

    function selectButton(index) {
      return selectButtons().eq(index);
    }
  });

});
