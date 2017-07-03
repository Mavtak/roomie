describe('angular roomie.devices color-switch-controls (directive)', function () {
  var $injector;
  var $scope;
  var buttons;
  var givenColorSwitch;

  beforeEach(angular.mock.module('roomie.devices', function ($provide) {
    $provide.value('ColorSwitchButtonGenerator', MockColorSwitchButtonGenerator);
  }));

  beforeEach(angular.mock.inject(function (_$injector_) {
    $injector = _$injector_;
    $scope = $injector.get('$rootScope').$new();
  }));

  beforeEach(function () {
    buttons = null;
    givenColorSwitch = null;
  });

  beforeEach(function () {
    $scope.colorSwitch = {};
  });

  it('gives the colorSwitch to the ColorSwitchButtonGenerator', function () {
    compileDirective();

    expect(givenColorSwitch).toBe($scope.colorSwitch);
  });

  describe('the button group', function () {

    it('has one', function () {
      var element = compileDirective();

      expect($(element).find('.buttonGroup').length).toEqual(1);
    });

    it('has as many buttons as the button generator returned', function () {
      buttons = [{}, {}, {}, {}, {}];

      var element = compileDirective();

      expect(selectButtons(element).length).toEqual(5);
    });

    it('pulls labels from the button generator', function () {
      buttons = [{
          label: 'herp'
        }, {
          label: 'derp'
        }, {
          label: ''
        }];

      var element = compileDirective();

      expect(selectButton(element, 0).text().trim()).toEqual('herp');
      expect(selectButton(element, 1).text().trim()).toEqual('derp');
      expect(selectButton(element, 2).text().trim()).toEqual('');
    });

    it('pulls activated from the button generator', function () {
      buttons = [{
          activated: true
        }, {
          activated: false
        }, {
          activated: true
        }];

      var element = compileDirective();

      expect(selectButton(element, 0).hasClass('activated')).toEqual(true);
      expect(selectButton(element, 1).hasClass('activated')).toEqual(false);
      expect(selectButton(element, 2).hasClass('activated')).toEqual(true);
    });

    it('pulls target color from the button generator (for rendering)', function () {
      buttons = [{
        color: 'orange'
      }, {
        color: 'green'
      }, {
        color: 'purple'
      }];

      var element = compileDirective();

      expect(selectButton(element, 0).css('background-color')).toEqual('orange');
      expect(selectButton(element, 1).css('background-color')).toEqual('green');
      expect(selectButton(element, 2).css('background-color')).toEqual('purple');
    });

    it('pulls target color from the button generator', function () {
      buttons = [{
          color: 'orange'
        }, {
          color: 'green'
        }, {
          color: 'purple'
        }];

      $scope.colorSwitch.setValue = jasmine.createSpy('setValue');

      var element = compileDirective();

      expect($scope.colorSwitch.setValue).not.toHaveBeenCalled();

      selectButton(element, 1).click();

      expect($scope.colorSwitch.setValue).toHaveBeenCalledWith('green');
    });

    function selectButtons(element) {
      return $(element).find('.buttonGroup .button').filter(':not(.button .button)');
    }

    function selectButton(element, index) {
      return selectButtons(element).eq(index).find('.button');
    }

  });

  function compileDirective(html) {
    html = html || '<color-switch-controls color-switch="colorSwitch"></color-switch-controls>';
    var $compile = $injector.get('$compile');
    var element = $compile(html)($scope);
    $scope.$digest();

    return element;
  }

  function MockColorSwitchButtonGenerator(colorSwitch) {
    givenColorSwitch = colorSwitch;

    this.generate = function () {
      return buttons;
    };
  }

});
