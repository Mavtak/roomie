﻿/// <reference path="../../../Scripts/Libraries/jquery-1.5.1.min.js"/>
/// <reference path="../../../Scripts/Libraries/angular-1.3.13.min.js"/>
/// <reference path="../angular-mocks.js"/>
/// <reference path="../../../Scripts/angular/dependencies.js"/>
/// <reference path="../../../Scripts/angular/common/widgetDataSection.js"/>

describe('roomie.common.widgetDataSection', function () {
  var $compile;
  var $rootScope;

  beforeEach(angular.mock.module('roomie.common'));

  beforeEach(angular.mock.inject(function ($injector) {
    $compile = $injector.get('$compile');
    $rootScope = $injector.get('$rootScope');
  }));

  it('works', function () {
    var element = $compile('<widget-data-section><div class="thingy">bam</div></widget-data-section>')($rootScope);
    $rootScope.$digest();

    expect($(element).find('.data .thingy').html()).toEqual('bam');
  });

});