/// <reference path="../../../Scripts/Libraries/jquery-1.5.1.min.js"/>
/// <reference path="../../../Scripts/Libraries/angular-1.3.13.min.js"/>
/// <reference path="../angular-mocks.js"/>
/// <reference path="../../../Scripts/angular/dependencies.js"/>
/// <reference path="../../../Scripts/angular/common/sideMenu.js"/>

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
    element = $compile('<side-menu item-selected="callback()"></side-menu>')($rootScope);

    $rootScope.$digest();
  });

  describe('the items', function() {

    it('there are two', function() {
      expect(selectItems().length).toEqual(2);
    });

    it('the first one is devices', function() {
      var item = selectItem(0);

      expect(item.attr('href')).toEqual('#devices');
      expect(item.find('.content').text()).toEqual('Devices');
    });

    it('the second one is tasks', function() {
      var item = selectItem(1);

      expect(item.attr('href')).toEqual('#tasks');
      expect(item.find('.content').text()).toEqual('Tasks');
    });

  });

  describe('the itemSelected function', function() {
    var callback;

    beforeEach(function() {
      callback = jasmine.createSpy();
      $rootScope.callback = callback;

      $rootScope.$digest();
    });

    it('is not called when the no item is clicked', function() {
      expect(callback).not.toHaveBeenCalled();
    });

    it('is called when the first item is clicked', function() {
      clickItem(0);

      expect(callback).toHaveBeenCalled();
    });

    it('is called when any item is clicked', function() {
      for (var i = 0; i < 2; i++) {
        clickItem(i);
      }

      window.callback = callback;

      expect(callback.calls.length).toEqual(2);
    });

    function clickItem(index) {
      angular.element(selectItem(index)).triggerHandler('click');
    }
  });

  function selectItems() {
    return $(element).find('.sideMenu .item');
  }

  function selectItem(index) {
    return selectItems().eq(index);
  }

});
