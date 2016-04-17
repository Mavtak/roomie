describe('angular roomie.common side-menu (directive)', function () {
  var $injector;
  var $scope;

  beforeEach(angular.mock.module('roomie.common'));

  beforeEach(angular.mock.inject(function (_$injector_) {
    $injector = _$injector_;
    $scope = $injector.get('$rootScope').$new();
  }));

  describe('the calculated-width attribute', function () {

    it('is set automatically', function () {
      compileDirective('<side-menu calculated-width="width"></side-menu>');

      expect($scope.width).toEqual(jasmine.any(String));
      expect($scope.width).toMatch(/^[0-9]*px$/);
    });

  });

  function compileDirective(html) {
    var $compile = $injector.get('$compile');
    var element = $compile(html)($scope);
    $scope.$digest();

    return element;
  }

});
