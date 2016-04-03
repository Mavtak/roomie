describe('angular roomie.common widget-header (directive)', function () {
  var $compile;
  var $rootScope;

  beforeEach(angular.mock.module('roomie.common'));

  beforeEach(angular.mock.inject(function ($injector) {
    $compile = $injector.get('$compile');
    $rootScope = $injector.get('$rootScope');
  }));

  beforeEach(function () {
    $rootScope.attributes = {};
  });

  it('works given a title', function () {
    var element = $compile('<widget-header title="herp"></widget-header>')($rootScope);
    $rootScope.$digest();

    expect($(element).find('.header')[0]).toBeDefined();
    expect($(element).find('.header').attr('href')).toEqual(undefined);
    expect($(element).find('.header .name').html().trim()).toEqual('herp');
    expect($(element).find('.header .location').html().trim()).toEqual('');
  });

  it('works given a title and subtitle', function () {
    var element = $compile('<widget-header title="herp" subtitle="derp"></widget-header>')($rootScope);
    $rootScope.$digest();

    expect($(element).find('.header')[0]).toBeDefined();
    expect($(element).find('.header').attr('href')).toEqual(undefined);
    expect($(element).find('.header .name').html().trim()).toEqual('herp');
    expect($(element).find('.header .location').html().trim()).toEqual('derp');
  });

  it('works given a title and subtitle and href', function () {
    var element = $compile('<widget-header title="herp" subtitle="derp" href="http://localhost/bam"></widget-header>')($rootScope);
    $rootScope.$digest();

    expect($(element).find('.header')[0]).toBeDefined();
    expect($(element).find('.header').attr('href')).toEqual('http://localhost/bam');
    expect($(element).find('.header .name').html().trim()).toEqual('herp');
    expect($(element).find('.header .location').html().trim()).toEqual('derp');
  });

  it('works given a title and href', function () {
    var element = $compile('<widget-header title="herp" href="http://localhost/bam"></widget-header>')($rootScope);
    $rootScope.$digest();

    expect($(element).find('.header')[0]).toBeDefined();
    expect($(element).find('.header').attr('href')).toEqual('http://localhost/bam');
    expect($(element).find('.header .name').html().trim()).toEqual('herp');
    expect($(element).find('.header .location').html().trim()).toEqual('');
  });

  describe('the disconnected attribute', function () {

    describe('when not specified', function () {

      it('does not display the disctonnected icon', function () {

        var element = $compile('<widget-header></widget-header>')($rootScope);
        $rootScope.$digest();

        expect($(element).find('.header > widget-disconnected-icon').length).toEqual(0);
      });

    });

    describe('when set to false', function () {

      it('does not display the disctonnected icon', function () {

        var element = $compile('<widget-header disconnected="false"></widget-header>')($rootScope);
        $rootScope.$digest();

        expect($(element).find('.header > widget-disconnected-icon').length).toEqual(0);
      });

    });

    describe('when set to true', function () {

      it('displays the disctonnected icon', function () {

        var element = $compile('<widget-header disconnected="true"></widget-header>')($rootScope);
        $rootScope.$digest();

        expect($(element).find('.header widget-disconnected-icon').length).toEqual(1);
      });

    });

    describe('when set to a scope variable that is not defined', function () {

      it('does not display the disctonnected icon', function () {

        var element = $compile('<widget-header disconnected="attributes.thing"></widget-header>')($rootScope);
        $rootScope.$digest();

        expect($(element).find('.header widget-disconnected-icon').length).toEqual(0);
      });

    });

    describe('when set to a scope variable that is set to false', function () {

      it('does not display the disctonnected icon', function () {

        var element = $compile('<widget-header disconnected="attributes.thing"></widget-header>')($rootScope);
        $rootScope.attributes.thing = false;
        $rootScope.$digest();

        expect($(element).find('.header widget-disconnected-icon').length).toEqual(0);
      });

    });

    describe('when set to a scope variable that is set to true', function () {

      it('displays the disctonnected icon', function () {

        var element = $compile('<widget-header disconnected="attributes.thing"></widget-header>')($rootScope);
        $rootScope.attributes.thing = true;
        $rootScope.$digest();

        expect($(element).find('.header widget-disconnected-icon').length).toEqual(1);
      });

    });

  });

});
