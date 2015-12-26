describe('roomie.common.sideMenu', function() {
  var $compile;
  var $rootScope;
  var element;

  beforeEach(angular.mock.module('roomie.common'));

  beforeEach(angular.mock.inject(function($injector) {
    $compile = $injector.get('$compile');
    $rootScope = $injector.get('$rootScope');
  }));

  beforeEach(function() {
    element = $compile('<side-menu calculated-width="width"></side-menu>')($rootScope);

    $rootScope.$digest();
  });

  describe('the calculated-width attribute', function() {

    it('is set automatically', function() {
      expect($rootScope.width).toEqual(jasmine.any(String));
      expect($rootScope.width).toMatch(/^[0-9]*px$/);
    });

  });

  function selectItems() {
    return $(element).find('.sideMenu .item');
  }

  function selectItem(index) {
    return selectItems().eq(index);
  }

});
