describe('angular roomie.common side-menu-item (directive)', function () {
  var $injector;
  var $scope;

  beforeEach(angular.mock.module('roomie.common'));

  beforeEach(angular.mock.inject(function (_$injector_) {
    $injector = _$injector_;
    $scope = $injector.get('$rootScope').$new();
  }));

  describe('the label attribute', function () {

    it('works when not set', function () {
      var element = compileDirective('<side-menu-item></side-menu-item>');

      expect($(element).find('.item .content').text().trim()).toEqual('');
    });

    it('works when set', function () {
      var element = compileDirective('<side-menu-item label="\'derp\'"></side-menu-item>');

      expect($(element).find('.item .content').text().trim()).toEqual('derp');
    });
  });

  describe('the target attribute', function () {

    it('works when not set', function () {
      var element = compileDirective('<side-menu-item></side-menu-item>');

      expect($(element).find('.item').attr('href')).toEqual('');
    });

    it('works when set', function () {
      var element = compileDirective('<side-menu-item target="\'derp\'"></side-menu-item>');

      expect($(element).find('.item').attr('href')).toEqual('derp');
    });
  });

  describe('the selected attribute', function () {

    it('works when not set up', function () {
      var element = compileDirective('<side-menu-item></side-menu-item>');

      $(element).find('.item').click();
    });

    //TODO: this test causes a reload, preventing other tests from running.  Maybe update the siteMenuItem template to use a button instead of a link.
    xit('works when set up correctly', function () {
      var element = compileDirective('<side-menu-item selected="thingy" target="\'javascript:void(0)\'"></side-menu-item>');

      var worked = false;

      $scope.thingy = function () {
        worked = true;
      };

      $scope.$digest();

      expect(worked).toEqual(false);

      $(element).find('.item')[0].click();

      expect(worked).toEqual(true);
    });

  });

  describe('the indent attribute', function () {

    it('works when not set', function () {
      var element = compileDirective('<side-menu-item label="\'derp\'"></side-menu-item>');

      expect($(element).find('.item').text().trim()).toEqual('derp');
    });

    it('works when set to 0', function () {
      var element = compileDirective('<side-menu-item indent="\'0\'" label="\'derp\'"></side-menu-item>');

      expect($(element).find('.item').text().replace(/[\r\n ]/g, '')).toEqual('derp');
    });

    it('works when set to 1', function () {
      var element = compileDirective('<side-menu-item indent="\'1\'" label="\'derp\'"></side-menu-item>');

      expect($(element).find('.item').text().replace(/[\r\n ]/g, '')).toEqual('\xA0\xA0derp');
    });

    it('works when set to 2', function () {
      var element = compileDirective('<side-menu-item indent="\'2\'" label="\'derp\'"></side-menu-item>');

      expect($(element).find('.item').text().replace(/[\r\n ]/g, '')).toEqual('\xA0\xA0\xA0\xA0derp');
    });

  });

  function compileDirective(html) {
    var $compile = $injector.get('$compile');
    var element = $compile(html)($scope);
    $scope.$digest();

    return element;
  }

});
