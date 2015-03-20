/// <reference path="../../../Scripts/Libraries/jquery-1.5.1.min.js"/>
/// <reference path="../../../Scripts/Libraries/angular-1.3.13.min.js"/>
/// <reference path="../angular-mocks.js"/>
/// <reference path="../../../Scripts/angular/dependencies.js"/>
/// <reference path="../../../Scripts/angular/common/widget.js"/>
/// <reference path="../../../Scripts/angular/common/widgetButton.js"/>
/// <reference path="../../../Scripts/angular/common/widgetButtonGroup.js"/>
/// <reference path="../../../Scripts/angular/devices/multilevelSwitchControls.js"/>

describe('roomie.devices.multilevelSwitchControls', function() {
  var $compile;
  var $rootScope;
  var element;
  var buttons;
  var givenMultilevelSwitch;
  var requestedCount;

  beforeEach(angular.mock.module('roomie.devices', function($provide) {
    $provide.factory('MultilevelSwitchButtonGenerator', function() {
      return MockMultilevelSwitchButtonGenerator;
    });
  }));

  beforeEach(angular.mock.inject(function ($injector) {
    $compile = $injector.get('$compile');
    $rootScope = $injector.get('$rootScope');
  }));

  beforeEach(function() {
    buttons = null;
    givenMultilevelSwitch = null;
    requestedCount = null;
  });

  beforeEach(function () {
    $rootScope.multilevelSwitch = {};
    element = $compile('<multilevel-switch-controls multilevel-switch="multilevelSwitch"></multilevel-switch-controls>')($rootScope);
  });

  it('gives the multilevelSwitch to the MultilevelSwitchButtonGenerator', function () {
    expect(givenMultilevelSwitch).toBe($rootScope.multilevelSwitch);
  });

  describe('the button group', function() {

    it('has one', function() {
      $rootScope.$digest();

      expect($(element).find('.buttonGroup').length).toEqual(1);
    });

    it('has 11 buttons', function() {
      $rootScope.$digest();

      expect(requestedCount).toEqual(11);
    });

    it('pulls labels from the button generator', function() {
      buttons = [{
          label: 'herp'
        }, {
          label: 'derp'
        }, {
          label: ''
        }];

      $rootScope.$digest();

      expect(selectButton(0).text()).toEqual('herp');
      expect(selectButton(1).text()).toEqual('derp');
      expect(selectButton(2).text()).toEqual('');
    });

    it('pulls activated from the button generator', function() {
      buttons = [{
          activated: true
        }, {
          activated: false
        }, {
          activated: true
        }];

      $rootScope.$digest();

      expect(selectButton(0).hasClass('activated')).toEqual(true);
      expect(selectButton(1).hasClass('activated')).toEqual(false);
      expect(selectButton(2).hasClass('activated')).toEqual(true);
    });

    it('pulls target power from the button generator', function() {
      buttons = [{
          power: 12
        }, {
          power: 34
        }, {
          power: 37
        }];

      $rootScope.multilevelSwitch.setPower = jasmine.createSpy('setPower');

      $rootScope.$digest();

      expect($rootScope.multilevelSwitch.setPower).not.toHaveBeenCalled();

      selectButton(1).click();

      expect($rootScope.multilevelSwitch.setPower).toHaveBeenCalledWith(34);
    });

    function selectButtons() {
      return $(element).find('.buttonGroup .button').filter(':not(.button .button)');
    }

    function selectButton(index) {
      return selectButtons().eq(index).find('.button');
    }
  });

  function MockMultilevelSwitchButtonGenerator(multilevelSwitch) {
    givenMultilevelSwitch = multilevelSwitch;

    this.generate = function(count) {
      requestedCount = count;
      return buttons;
    };
  }
});
