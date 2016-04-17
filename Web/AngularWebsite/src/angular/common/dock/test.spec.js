describe('angular roomie.common dock (directive)', function () {
  var $injector;
  var $scope;

  beforeEach(angular.mock.module('roomie.common'));

  beforeEach(angular.mock.inject(function (_$injector_) {
    $injector = _$injector_;
    $scope = $injector.get('$rootScope').$new();
  }));

  describe('dock styling', function () {
    var element;

    beforeEach(function () {
      element = compileDirective('<dock area="blam"><div class="boop">beep</div></dock>');
    });

    it('exists', function () {
      expect(selectDock().length).toEqual(1);
    });

    it('has a "dock" class', function () {
      expect(selectDock().hasClass('dock')).toEqual(true);
    });

    it('has a class that matches the "area" attribute', function () {
      expect(selectDock().hasClass('blam')).toEqual(true);
    });

    function selectDock() {
      return $(element).children().eq(0);
    }

  });

  describe('the filler', function () {
    var element;

    beforeEach(function () {
      element = compileDirective('<dock area="blam"><div class="boop">beep</div></dock>');
    });

    it('exists', function () {
      expect(selectFiller().length).toEqual(1);
    });

    it('has a custom style that matches its height to the dock height', function () {
      expect(selectFiller().attr('style')).toMatch(/height\: [0-9]+px/);
    });

    function selectFiller() {
      return $(element).children().eq(1);
    }

  });

  describe('pixel height binding', function () {
    var element;

    beforeEach(function () {
      element = compileDirective('<dock area="blam" pixel-height="attributes.height"><div class="boop">beep</div></dock>');
    });

    it('sets the pixel-height value', function () {
      expect($scope.attributes.height).toMatch(/[0-9]+/);
    });

  });

  function compileDirective(html) {
      var $compile = $injector.get('$compile');
      var element = $compile(html)($scope);
      $scope.$digest();

      return element;
  }

});
