/// <reference path="../../../Scripts/Libraries/jquery-1.5.1.min.js"/>
/// <reference path="../../../Scripts/Libraries/angular-1.3.13.min.js"/>
/// <reference path="../angular-mocks.js"/>
/// <reference path="../../../Scripts/angular/dependencies.js"/>
/// <reference path="../../../Scripts/angular/common/widgetHeader.js"/>
/// <reference path="../../../Scripts/angular/devices/deviceWidget.js"/>
/// <reference path="../../../Scripts/angular/devices/deviceList.js"/>

describe('roomie.devices.deviceList', function() {
  var $compile;
  var $rootScope;
  var element;

  beforeEach(angular.mock.module('roomie.devices'));

  beforeEach(angular.mock.inject(function(_$compile_, _$rootScope_) {
    $compile = _$compile_;
    $rootScope = _$rootScope_;
  }));

  beforeEach(function() {
    element = $compile('<device-list devices="page.items"></device-list>')($rootScope);

    $rootScope.page = {
      items: []
    };
  });

  it('lists out the items', function() {
    $rootScope.$digest();

    expect(selectDeviceWidgets().length).toEqual(0);

    $rootScope.page.items.push({
      name: 'device 1'
    });
    $rootScope.page.items.push({
      name: 'device 2'
    });
    $rootScope.$digest();

    expect(selectDeviceWidgets().length).toEqual(2);
    expect(selectDeviceWidget(0).find('widget-header .header .name').html()).toEqual('device 1');
    expect(selectDeviceWidget(1).find('widget-header .header .name').html()).toEqual('device 2');

    $rootScope.page.items.push({
      name: 'device 3'
    });
    $rootScope.$digest();

    expect(selectDeviceWidgets().length).toEqual(3);
    expect(selectDeviceWidget(0).find('widget-header .header .name').html()).toEqual('device 1');
    expect(selectDeviceWidget(1).find('widget-header .header .name').html()).toEqual('device 2');
    expect(selectDeviceWidget(2).find('widget-header .header .name').html()).toEqual('device 3');
  });

  function selectDeviceWidgets() {
    return $(element).find('device-widget');
  }

  function selectDeviceWidget(index) {
    return selectDeviceWidgets().eq(index);
  }
});
