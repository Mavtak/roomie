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
    $rootScope.$digest();
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

      expect(selectButton(0).text().trim()).toEqual('herp');
      expect(selectButton(1).text().trim()).toEqual('derp');
      expect(selectButton(2).text().trim()).toEqual('');
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
