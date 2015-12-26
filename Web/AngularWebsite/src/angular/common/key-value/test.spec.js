describe('roomie.common.keyValue', function () {
  var $compile;
  var $rootScope;

  beforeEach(angular.mock.module('roomie.common'));

  beforeEach(angular.mock.inject(function ($injector) {
    $compile = $injector.get('$compile');
    $rootScope = $injector.get('$rootScope');
  }));

  it('works given a key and value', function () {
    var element = $compile('<key-value key="herp" value="derp"></key-value>')($rootScope);
    $rootScope.$digest();

    expect($(element).find('.item')[0]).toBeDefined();
    expect($(element).find('.item .key').html()).toEqual('herp:');
    expect($(element).find('.item .value').html()).toEqual('derp');
    expect($(element).find('.item .value').attr('href')).toEqual(undefined);
  });

  it('works given a key and value and href', function () {
    var element = $compile('<key-value key="herp" value="derp" href="http://localhost/bam"></key-value>')($rootScope);
    $rootScope.$digest();

    expect($(element).find('.item')[0]).toBeDefined();
    expect($(element).find('.item .key').html()).toEqual('herp:');
    expect($(element).find('.item .value').html()).toEqual('derp');
    expect($(element).find('.item .value').attr('href')).toEqual('http://localhost/bam');
  });

});
