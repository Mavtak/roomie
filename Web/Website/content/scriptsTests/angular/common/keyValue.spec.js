/// <reference path="../../../Scripts/Libraries/jquery-1.5.1.min.js"/>
/// <reference path="../../../Scripts/Libraries/angular.js"/>
/// <reference path="../angular-mocks.js"/>
/// <reference path="../../../Scripts/angular/dependencies.js"/>
/// <reference path="../../../Scripts/angular/common/keyValue.js"/>

describe('roomie.common.keyValue', function () {
  var $compile;
  var $rootScope;

  beforeEach(angular.mock.module('roomie.common'));

  beforeEach(angular.mock.inject(function (_$compile_, _$rootScope_) {
    $compile = _$compile_;
    $rootScope = _$rootScope_;
  }));

  it('works given a key and value', function () {
    var element = $compile('<key-value key="herp" value="derp"></key-value>')($rootScope);
    $rootScope.$digest();

    expect($(element).find('.item')[0]).toBeDefined();
    expect($(element).find('.item .key').html()).toEqual('herp:');
    expect($(element).find('.item .value').html()).toEqual('derp');
    expect($(element).find('.item .value').attr('href')).toEqual(null);
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