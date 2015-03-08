/// <reference path="../../../Scripts/Libraries/jquery-1.5.1.min.js"/>
/// <reference path="../../../Scripts/Libraries/angular-1.3.13.min.js"/>
/// <reference path="../angular-mocks.js"/>
/// <reference path="../../../Scripts/angular/dependencies.js"/>
/// <reference path="../../../Scripts/angular/common/widgetButton.js"/>

describe('roomie.common.widgetButton', function() {
  var $compile;
  var $rootScope;

  beforeEach(angular.mock.module('roomie.common'));

  beforeEach(angular.mock.inject(function(_$compile_, _$rootScope_) {
    $compile = _$compile_;
    $rootScope = _$rootScope_;
  }));

  describe('the label', function() {

    it('works when not set', function () {
      var element = $compile('<widget-button></widget-button>')($rootScope);

      $rootScope.$digest();

      expect($(element).find('.button .button').html()).toEqual('');
    });

    it('works when set', function() {
      var element = $compile('<widget-button label="derp"></widget-button>')($rootScope);

      $rootScope.$digest();

      expect($(element).find('.button .button').html()).toEqual('derp');
    });

  });

  describe('the activation event', function() {

    it('works when set up correctly', function() {
      var element = $compile('<widget-button activate="thingy()"></widget-button>')($rootScope);

      var worked = false;

      $rootScope.thingy = function() {
        worked = true;
      };

      $rootScope.$digest();

      expect(worked).toEqual(false);

      $(element).find('.button .button').click();

      expect(worked).toEqual(true);
    });

    it('works when not set up', function() {
      var element = $compile('<widget-button"></widget-button>')($rootScope);

      $(element).find('.button .button').click();
    });

  });

  describe('the activated styling', function() {

    it('works when not set', function() {
      var element = $compile('<widget-button></widget-button>')($rootScope);

      $rootScope.$digest();

      expect($(element).find('.button .button.activated').length).toEqual(0);
    });

    it('works when set to false', function() {
      var element = $compile('<widget-button activated="false"></widget-button>')($rootScope);

      $rootScope.$digest();

      expect($(element).find('.button .button.activated').length).toEqual(0);
    });

    it('works when set to true', function() {
      var element = $compile('<widget-button activated="true"></widget-button>')($rootScope);

      $rootScope.$digest();

      expect($(element).find('.button .button.activated').length).toEqual(1);
    });

  });

});
