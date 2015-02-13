/// <reference path="../../../Scripts/Libraries/jquery-1.5.1.min.js"/>
/// <reference path="../../../Scripts/Libraries/angular.js"/>
/// <reference path="../angular-mocks.js"/>
/// <reference path="../../../Scripts/angular/dependencies.js"/>
/// <reference path="../../../Scripts/angular/common/widgetDataSection.js"/>

describe('roomie.common.widgetDataSection', function () {
  var $compile;
  var $rootScope;

  beforeEach(angular.mock.module('roomie.common'));

  beforeEach(angular.mock.inject(function (_$compile_, _$rootScope_) {
    $compile = _$compile_;
    $rootScope = _$rootScope_;
  }));

  it('works', function () {
    var element = $compile('<widget-data-section><div class="thingy">bam</div></widget-data-section>')($rootScope);
    $rootScope.$digest();

    expect($(element).find('.data .thingy').html()).toEqual('bam');
  });

});