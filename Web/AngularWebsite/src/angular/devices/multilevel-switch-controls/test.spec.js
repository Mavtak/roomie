describe('angular roomie.devices multilevel-switch-controls (directive)', function () {
  var $injector;
  var $scope;
  var buttons;
  var givenMultilevelSwitch;
  var requestedCount;

  beforeEach(angular.mock.module('roomie.devices', function ($provide) {
    $provide.value('MultilevelSwitchButtonGenerator', MockMultilevelSwitchButtonGenerator);
  }));

  beforeEach(angular.mock.inject(function (_$injector_) {
    $injector = _$injector_;
    $scope = $injector.get('$rootScope').$new();
  }));

  beforeEach(function () {
    buttons = null;
    givenMultilevelSwitch = null;
    requestedCount = null;
  });

  beforeEach(function () {
    $scope.multilevelSwitch = {};
  });

  it('gives the multilevelSwitch to the MultilevelSwitchButtonGenerator', function () {
    compileDirective();

    expect(givenMultilevelSwitch).toBe($scope.multilevelSwitch);
  });

  describe('the button group', function () {

    it('has one', function () {
      var element = compileDirective();

      expect($(element).find('.buttonGroup').length).toEqual(1);
    });

    it('has 11 buttons', function () {
      var element = compileDirective();

      expect(requestedCount).toEqual(11);
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

    it('pulls target power from the button generator', function () {
      buttons = [{
          power: 12
        }, {
          power: 34
        }, {
          power: 37
        }];

      $scope.multilevelSwitch.setPower = jasmine.createSpy('setPower');

      var element = compileDirective();

      expect($scope.multilevelSwitch.setPower).not.toHaveBeenCalled();

      selectButton(element, 1).click();

      expect($scope.multilevelSwitch.setPower).toHaveBeenCalledWith(34);
    });

    function selectButtons(element) {
      return $(element).find('.buttonGroup .button').filter(':not(.button .button)');
    }

    function selectButton(element, index) {
      return selectButtons(element).eq(index).find('.button');
    }

  });

  function compileDirective(html) {
    html = html || '<multilevel-switch-controls multilevel-switch="multilevelSwitch"></multilevel-switch-controls>';
    var $compile = $injector.get('$compile');
    var element = $compile(html)($scope);
    $scope.$digest();

    return element;
  }

  function MockMultilevelSwitchButtonGenerator(multilevelSwitch) {
    givenMultilevelSwitch = multilevelSwitch;

    this.generate = function (count) {
      requestedCount = count;
      return buttons;
    };
  }

});
