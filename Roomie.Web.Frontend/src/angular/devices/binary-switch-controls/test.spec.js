describe('angular roomie.devices binary-switch-controls (directive)', function () {
  var $injector;
  var $scope;
  var element;

  beforeEach(angular.mock.module('roomie.devices'));

  beforeEach(angular.mock.inject(function (_$injector_) {
    $injector = _$injector_;
    $scope = $injector.get('$rootScope').$new();
  }));

  beforeEach(function () {
    $scope.binarySwitch = {};

    element = compileDirective('<binary-switch-controls binary-switch="binarySwitch"></binary-switch-controls>');
  });

  describe('the button group', function () {

    it('has one', function () {
      $scope.$digest();

      expect($(element).find('.buttonGroup').length).toEqual(1);
    });

    it('has two buttons', function () {
      $scope.$digest();

      expect(selectButtons().length).toEqual(2);
    });

    describe('the off button', function () {

      it('is the first button (identified by the label)', function () {
        $scope.$digest();

        expect(selectButton(0).html().trim()).toEqual('Off');
      });

      describe('activation styling', function () {

        it('yes when binarySwitch.power is off', function () {
          $scope.binarySwitch.power = "off";
          $scope.$digest();

          expect(selectButton(0).hasClass('activated')).toEqual(true);
        });

        it('no when binarySwitch.power is on', function () {
          $scope.binarySwitch.power = "on";
          $scope.$digest();

          expect(selectButton(0).hasClass('activated')).toEqual(false);
        });

        it('no when binarySwitch.power is Off (upper case)', function () {
          $scope.binarySwitch.power = "Off";
          $scope.$digest();

          expect(selectButton(0).hasClass('activated')).toEqual(false);
        });

        it('no when binarySwitch.power is something unexpected', function () {
          $scope.binarySwitch.power = "blam";
          $scope.$digest();

          expect(selectButton(0).hasClass('activated')).toEqual(false);
        });

        it('no when binarySwitch.power is undefined', function () {
          $scope.$digest();

          expect(selectButton(1).hasClass('activated')).toEqual(false);
        });

      });

      describe('clicking', function () {

        it('calls binarySwitch.setPower("On") when clicked', function () {
          $scope.binarySwitch.setPower = jasmine.createSpy('setPower');
          $scope.$digest();

          expect($scope.binarySwitch.setPower.calls.count()).toEqual(0);

          selectButton(0).click();

          expect($scope.binarySwitch.setPower.calls.count()).toEqual(1);
        });

        it('if fine if binarySwitch.setPower does not exist when clicked', function () {
          $scope.$digest();

          selectButton(0).click();
        });

      });

    });

    describe('the on button', function () {

      it('is the second button (identified by the label)', function () {
        $scope.$digest();

        expect(selectButton(1).html().trim()).toEqual('On');
      });

      describe('activation styling', function () {

        it('yes when binarySwitch.power is on', function () {
          $scope.binarySwitch.power = "on";
          $scope.$digest();

          expect(selectButton(1).hasClass('activated')).toEqual(true);
        });

        it('no when binarySwitch.power is off', function () {
          $scope.binarySwitch.power = "off";
          $scope.$digest();

          expect(selectButton(1).hasClass('activated')).toEqual(false);
        });

        it('no when binarySwitch.power is On (upper case)', function () {
          $scope.binarySwitch.power = "On";
          $scope.$digest();

          expect(selectButton(1).hasClass('activated')).toEqual(false);
        });

        it('no when binarySwitch.power is something unexpected', function () {
          $scope.binarySwitch.power = "blam";
          $scope.$digest();

          expect(selectButton(1).hasClass('activated')).toEqual(false);
        });

        it('no when binarySwitch.power is undefined', function () {
          $scope.$digest();

          expect(selectButton(1).hasClass('activated')).toEqual(false);
        });

      });

      describe('clicking', function () {

        it('calls binarySwitch.setPower("On") when clicked', function () {
          $scope.binarySwitch.setPower = jasmine.createSpy('setPower');
          $scope.$digest();

          expect($scope.binarySwitch.setPower.calls.count()).toEqual(0);

          selectButton(1).click();

          expect($scope.binarySwitch.setPower.calls.count()).toEqual(1);
        });

        it('if fine if binarySwitch.setPower does not exist when clicked', function () {
          $scope.$digest();

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

  function compileDirective(html) {
    var $compile = $injector.get('$compile');
    var element = $compile(html)($scope);
    $scope.$digest();

    return element;
  }

});
