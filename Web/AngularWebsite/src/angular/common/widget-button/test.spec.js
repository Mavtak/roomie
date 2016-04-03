describe('angular roomie.common widget-button (directive)', function () {
  var $compile;
  var $rootScope;

  beforeEach(angular.mock.module('roomie.common'));

  beforeEach(angular.mock.inject(function ($injector) {
    $compile = $injector.get('$compile');
    $rootScope = $injector.get('$rootScope');
  }));

  describe('the label', function () {

    it('works when not set', function () {
      var element = $compile('<widget-button></widget-button>')($rootScope);

      $rootScope.$digest();

      expect($(element).find('.button .button').html().trim()).toEqual('');
    });

    it('works when set', function () {
      var element = $compile('<widget-button label="derp"></widget-button>')($rootScope);

      $rootScope.$digest();

      expect($(element).find('.button .button').html().trim()).toEqual('derp');
    });

  });

  describe('the activation event', function () {

    it('works when set up correctly', function () {
      var element = $compile('<widget-button activate="thingy()"></widget-button>')($rootScope);

      var worked = false;

      $rootScope.thingy = function () {
        worked = true;
      };

      $rootScope.$digest();

      expect(worked).toEqual(false);

      $(element).find('.button .button').click();

      expect(worked).toEqual(true);
    });

    it('works when not set up', function () {
      var element = $compile('<widget-button"></widget-button>')($rootScope);

      $(element).find('.button .button').click();
    });

  });

  describe('the activated styling', function () {

    it('works when not set', function () {
      var element = $compile('<widget-button></widget-button>')($rootScope);

      $rootScope.$digest();

      expect($(element).find('.button .button.activated').length).toEqual(0);
    });

    it('works when set to false', function () {
      var element = $compile('<widget-button activated="false"></widget-button>')($rootScope);

      $rootScope.$digest();

      expect($(element).find('.button .button.activated').length).toEqual(0);
    });

    it('works when set to true', function () {
      var element = $compile('<widget-button activated="true"></widget-button>')($rootScope);

      $rootScope.$digest();

      expect($(element).find('.button .button.activated').length).toEqual(1);
    });

  });

  describe('the coloring', function () {

    it('works when not set', function () {
      var element = $compile('<widget-button></widget-button>')($rootScope);

      $rootScope.$digest();

      expect($(element).find('.button .button').css('background-color')).toEqual('');
    });

    it('works when set', function () {
      var element = $compile('<widget-button color="red"></widget-button>')($rootScope);

      $rootScope.$digest();

      expect($(element).find('.button .button').css('background-color')).toEqual('red');
    });

    it('works when changed', function () {
      $rootScope.color = 'red';
      var element = $compile('<widget-button color="{{color}}"></widget-button>')($rootScope);

      $rootScope.$digest();
      expect($(element).find('.button .button').css('background-color')).toEqual('red');

      $rootScope.color = 'blue';
      $rootScope.$digest();

      expect($(element).find('.button .button').css('background-color')).toEqual('blue');
    });

  });

});
