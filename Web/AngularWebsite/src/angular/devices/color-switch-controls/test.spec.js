describe('angular roomie.devices color-switch-controls (directive)', function() {
  var $compile;
  var $rootScope;
  var element;
  var buttons;
  var givenColorSwitch;

  beforeEach(angular.mock.module('roomie.devices', function($provide) {
    $provide.factory('ColorSwitchButtonGenerator', function() {
      return MockColorSwitchButtonGenerator;
    });
  }));

  beforeEach(angular.mock.inject(function ($injector) {
    $compile = $injector.get('$compile');
    $rootScope = $injector.get('$rootScope');
  }));

  beforeEach(function() {
    buttons = null;
    givenColorSwitch = null;
  });

  beforeEach(function () {
    $rootScope.colorSwitch = {};
    element = $compile('<color-switch-controls color-switch="colorSwitch"></color-switch-controls>')($rootScope);
  });

  it('gives the colorSwitch to the ColorSwitchButtonGenerator', function () {
    $rootScope.$digest();
    expect(givenColorSwitch).toBe($rootScope.colorSwitch);
  });

  describe('the button group', function() {

    it('has one', function() {
      $rootScope.$digest();

      expect($(element).find('.buttonGroup').length).toEqual(1);
    });

    it('has as many buttons as the button generator returned', function () {
      buttons = [{}, {}, {}, {}, {}];
      $rootScope.$digest();

      expect(selectButtons().length).toEqual(5);
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

    it('pulls target color from the button generator (for rendering)', function () {
      buttons = [{
        color: 'orange'
      }, {
        color: 'green'
      }, {
        color: 'purple'
      }];

      $rootScope.$digest();

      expect(selectButton(0).css('background-color')).toEqual('orange');
      expect(selectButton(1).css('background-color')).toEqual('green');
      expect(selectButton(2).css('background-color')).toEqual('purple');
    });

    it('pulls target color from the button generator', function() {
      buttons = [{
          color: 'orange'
        }, {
          color: 'green'
        }, {
          color: 'purple'
        }];

      $rootScope.colorSwitch.setValue = jasmine.createSpy('setValue');

      $rootScope.$digest();

      expect($rootScope.colorSwitch.setValue).not.toHaveBeenCalled();

      selectButton(1).click();

      expect($rootScope.colorSwitch.setValue).toHaveBeenCalledWith('green');
    });

    function selectButtons() {
      return $(element).find('.buttonGroup .button').filter(':not(.button .button)');
    }

    function selectButton(index) {
      return selectButtons().eq(index).find('.button');
    }
  });

  function MockColorSwitchButtonGenerator(colorSwitch) {
    givenColorSwitch = colorSwitch;

    this.generate = function() {
      return buttons;
    };
  }
});
