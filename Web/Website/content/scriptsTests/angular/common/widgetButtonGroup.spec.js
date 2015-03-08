/// <reference path="../../../Scripts/Libraries/jquery-1.5.1.min.js"/>
/// <reference path="../../../Scripts/Libraries/angular-1.3.13.min.js"/>
/// <reference path="../angular-mocks.js"/>
/// <reference path="../../../Scripts/angular/dependencies.js"/>
/// <reference path="../../../Scripts/angular/common/widgetButtonGroup.js"/>

describe('roomie.common.widgetButtonGroup', function () {
  var $compile;
  var $rootScope;

  beforeEach(angular.mock.module('roomie.common'));

  beforeEach(angular.mock.inject(function (_$compile_, _$rootScope_) {
    $compile = _$compile_;
    $rootScope = _$rootScope_;
  }));

  it('works', function () {
    var element = $compile('<widget-button-group><div class="thingy">bam</div></widget-button-group>')($rootScope);
    $rootScope.$digest();

    expect($(element).find('.buttonGroup .thingy').html()).toEqual('bam');
  });

});