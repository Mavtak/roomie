describe('roomie.common.dock', function () {
  var $compile;
  var $rootScope;
  var element;

  beforeEach(angular.mock.module('roomie.common'));

  beforeEach(angular.mock.inject(function ($injector) {
    $compile = $injector.get('$compile');
    $rootScope = $injector.get('$rootScope');
    element = $compile('<dock area="blam"><div class="boop">beep</div></dock>')($rootScope);

    attributes = {};
    $rootScope.attributes = attributes;
    $rootScope.$digest();
  }));

  describe('dock styling', function() {

    it('exists', function() {
      expect(selectDock().length).toEqual(1);
    });

    it('has a "dock" class', function () {
      expect(selectDock().hasClass('dock')).toEqual(true);
    });

    it('has a class that matches the "area" attribute', function () {
      expect(selectDock().hasClass('blam')).toEqual(true);
    });

  });

  describe('the filler', function () {

    it('exists', function () {
      expect(selectFiller().length).toEqual(1);
    });

    it('has a custom style that matches its height to the dock height', function() {
      expect(selectFiller().attr('style')).toMatch(/height\: [0-9]+px/);
    });

  });

  describe('pixel height binding', function() {

    beforeEach(function() {
      element = $compile('<dock area="blam" pixel-height="attributes.height"><div class="boop">beep</div></dock>')($rootScope);
      $rootScope.$digest();
    });

    it('sets the pixel-height value', function() {
      expect(attributes.height).toMatch(/[0-9]+/);
    });

  });

  function selectDock() {
    return $(element).children().eq(0);
  }

  function selectFiller() {
    return $(element).children().eq(1);
  }

});
