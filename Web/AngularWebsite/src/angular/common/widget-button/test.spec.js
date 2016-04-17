describe('angular roomie.common widget-button (directive)', function () {
  var $injector;
  var $scope;

  beforeEach(angular.mock.module('roomie.common'));

  beforeEach(angular.mock.inject(function (_$injector_) {
    $injector = _$injector_;
    $scope = $injector.get('$rootScope').$new();
  }));

  describe('the label', function () {

    it('works when not set', function () {
      var element = compileDirective('<widget-button></widget-button>');

      expect($(element).find('.button .button').html().trim()).toEqual('');
    });

    it('works when set', function () {
      var element = compileDirective('<widget-button label="derp"></widget-button>');

      expect($(element).find('.button .button').html().trim()).toEqual('derp');
    });

  });

  describe('the activation event', function () {

    it('works when set up correctly', function () {
      var worked = false;

      $scope.thingy = function () {
        worked = true;
      };

      var element = compileDirective('<widget-button activate="thingy()"></widget-button>');

      expect(worked).toEqual(false);

      $(element).find('.button .button').click();

      expect(worked).toEqual(true);
    });

    it('works when not set up', function () {
      var element = compileDirective('<widget-button"></widget-button>');

      $(element).find('.button .button').click();
    });

  });

  describe('the activated styling', function () {

    it('works when not set', function () {
      var element = compileDirective('<widget-button></widget-button>');

      expect($(element).find('.button .button.activated').length).toEqual(0);
    });

    it('works when set to false', function () {
      var element = compileDirective('<widget-button activated="false"></widget-button>');

      expect($(element).find('.button .button.activated').length).toEqual(0);
    });

    it('works when set to true', function () {
      var element = compileDirective('<widget-button activated="true"></widget-button>');

      expect($(element).find('.button .button.activated').length).toEqual(1);
    });

  });

  describe('the coloring', function () {

    it('works when not set', function () {
      var element = compileDirective('<widget-button></widget-button>');

      expect($(element).find('.button .button').css('background-color')).toEqual('');
    });

    it('works when set', function () {
      var element = compileDirective('<widget-button color="red"></widget-button>');

      expect($(element).find('.button .button').css('background-color')).toEqual('red');
    });

    it('works when changed', function () {
      $scope.color = 'red';
      var element = compileDirective('<widget-button color="{{color}}"></widget-button>');

      expect($(element).find('.button .button').css('background-color')).toEqual('red');

      $scope.color = 'blue';
      $scope.$digest();

      expect($(element).find('.button .button').css('background-color')).toEqual('blue');
    });

  });

  function compileDirective(html) {
    var $compile = $injector.get('$compile');
    var element = $compile(html)($scope);
    $scope.$digest();

    return element;
  }

});
