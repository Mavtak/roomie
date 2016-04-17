describe('angular roomie.common widget-header (directive)', function () {
  var $injector;
  var $scope;

  beforeEach(angular.mock.module('roomie.common'));

  beforeEach(angular.mock.inject(function (_$injector_) {
    $injector = _$injector_;
    $scope = $injector.get('$rootScope').$new();
  }));

  beforeEach(function () {
    $scope.attributes = {};
  });

  it('works given a title', function () {
    var element = compileDirective('<widget-header title="herp"></widget-header>');

    expect($(element).find('.header')[0]).toBeDefined();
    expect($(element).find('.header').attr('href')).toEqual(undefined);
    expect($(element).find('.header .name').html().trim()).toEqual('herp');
    expect($(element).find('.header .location').html().trim()).toEqual('');
  });

  it('works given a title and subtitle', function () {
    var element = compileDirective('<widget-header title="herp" subtitle="derp"></widget-header>');

    expect($(element).find('.header')[0]).toBeDefined();
    expect($(element).find('.header').attr('href')).toEqual(undefined);
    expect($(element).find('.header .name').html().trim()).toEqual('herp');
    expect($(element).find('.header .location').html().trim()).toEqual('derp');
  });

  it('works given a title and subtitle and href', function () {
    var element = compileDirective('<widget-header title="herp" subtitle="derp" href="http://localhost/bam"></widget-header>');

    expect($(element).find('.header')[0]).toBeDefined();
    expect($(element).find('.header').attr('href')).toEqual('http://localhost/bam');
    expect($(element).find('.header .name').html().trim()).toEqual('herp');
    expect($(element).find('.header .location').html().trim()).toEqual('derp');
  });

  it('works given a title and href', function () {
    var element = compileDirective('<widget-header title="herp" href="http://localhost/bam"></widget-header>');

    expect($(element).find('.header')[0]).toBeDefined();
    expect($(element).find('.header').attr('href')).toEqual('http://localhost/bam');
    expect($(element).find('.header .name').html().trim()).toEqual('herp');
    expect($(element).find('.header .location').html().trim()).toEqual('');
  });

  describe('the disconnected attribute', function () {

    describe('when not specified', function () {

      it('does not display the disctonnected icon', function () {

        var element = compileDirective('<widget-header></widget-header>');

        expect($(element).find('.header > widget-disconnected-icon').length).toEqual(0);
      });

    });

    describe('when set to false', function () {

      it('does not display the disctonnected icon', function () {
        var element = compileDirective('<widget-header disconnected="false"></widget-header>');

        expect($(element).find('.header > widget-disconnected-icon').length).toEqual(0);
      });

    });

    describe('when set to true', function () {

      it('displays the disctonnected icon', function () {

        var element = compileDirective('<widget-header disconnected="true"></widget-header>');

        expect($(element).find('.header widget-disconnected-icon').length).toEqual(1);
      });

    });

    describe('when set to a scope variable that is not defined', function () {

      it('does not display the disctonnected icon', function () {

        var element = compileDirective('<widget-header disconnected="attributes.thing"></widget-header>');

        expect($(element).find('.header widget-disconnected-icon').length).toEqual(0);
      });

    });

    describe('when set to a scope variable that is set to false', function () {

      it('does not display the disctonnected icon', function () {
        $scope.attributes.thing = false;

        var element = compileDirective('<widget-header disconnected="attributes.thing"></widget-header>');

        expect($(element).find('.header widget-disconnected-icon').length).toEqual(0);
      });

    });

    describe('when set to a scope variable that is set to true', function () {

      it('displays the disctonnected icon', function () {
        $scope.attributes.thing = true;

        var element = compileDirective('<widget-header disconnected="attributes.thing"></widget-header>');

        expect($(element).find('.header widget-disconnected-icon').length).toEqual(1);
      });

    });

  });

  function compileDirective(html) {
    var $compile = $injector.get('$compile');
    var element = $compile(html)($scope);
    $scope.$digest();

    return element;
  }

});
