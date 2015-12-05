/// <reference path="../../../Scripts/Libraries/jquery-1.5.1.min.js"/>
/// <reference path="../../../Scripts/Libraries/angular-1.3.13.min.js"/>
/// <reference path="../angular-mocks.js"/>
/// <reference path="../../../Scripts/angular/dependencies.js"/>
/// <reference path="../../../Scripts/angular/common/sideMenuItem.js"/>

describe('roomie.common.sideMenuItem', function() {
  var $compile;
  var $rootScope;

  beforeEach(angular.mock.module('roomie.common'));

  beforeEach(angular.mock.inject(function($injector) {
    $compile = $injector.get('$compile');
    $rootScope = $injector.get('$rootScope');
  }));

  describe('the label attribute', function() {

    it('works when not set', function() {
      var element = $compile('<side-menu-item></side-menu-item>')($rootScope);

      $rootScope.$digest();

      expect($(element).find('.item .content').text()).toEqual('');
    });

    it('works when set', function() {
      var element = $compile('<side-menu-item label="\'derp\'"></side-menu-item>')($rootScope);

      $rootScope.$digest();

      expect($(element).find('.item .content').text()).toEqual('derp');
    });
  });

  describe('the target attribute', function() {

    it('works when not set', function() {
      var element = $compile('<side-menu-item></side-menu-item>')($rootScope);

      $rootScope.$digest();

      expect($(element).find('.item').attr('href')).toEqual('');
    });

    it('works when set', function() {
      var element = $compile('<side-menu-item target="\'derp\'"></side-menu-item>')($rootScope);

      $rootScope.$digest();

      expect($(element).find('.item').attr('href')).toEqual('derp');
    });
  });

  describe('the selected attribute', function() {

    it('works when not set up', function() {
      var element = $compile('<side-menu-item></side-menu-item>')($rootScope);

      $(element).find('.item').click();
    });

    it('works when set up correctly', function() {
      var element = $compile('<side-menu-item selected="thingy" target="\'javascript:void(0)\'"></side-menu-item>')($rootScope);

      var worked = false;

      $rootScope.thingy = function () {
        worked = true;
      };

      $rootScope.$digest();
      window.element = element;
      expect(worked).toEqual(false);

      $(element).find('.item')[0].click();

      expect(worked).toEqual(true);
    });

  });

  describe('the indent attribute', function () {

    it('works when not set', function () {
      var element = $compile('<side-menu-item label="\'derp\'"></side-menu-item>')($rootScope);

      $rootScope.$digest();

      expect($(element).find('.item').text()).toEqual('derp');
    });

    it('works when set to 0', function () {
      var element = $compile('<side-menu-item indent="\'0\'" label="\'derp\'"></side-menu-item>')($rootScope);

      $rootScope.$digest();

      expect($(element).find('.item').text()).toEqual('derp');
    });
    
    it('works when set to 1', function () {
      var element = $compile('<side-menu-item indent="\'1\'" label="\'derp\'"></side-menu-item>')($rootScope);

      $rootScope.$digest();

      expect($(element).find('.item').text()).toEqual('\xA0\xA0derp');
    });
    
    it('works when set to 2', function () {
      var element = $compile('<side-menu-item indent="\'2\'" label="\'derp\'"></side-menu-item>')($rootScope);

      $rootScope.$digest();

      expect($(element).find('.item').text()).toEqual('\xA0\xA0\xA0\xA0derp');
    });
    
  });

});
