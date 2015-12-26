describe('roomie.devices.deviceList', function() {
  var $compile;
  var $rootScope;
  var element;

  beforeEach(angular.mock.module('roomie.devices'));

  beforeEach(angular.mock.inject(function ($injector) {
    $compile = $injector.get('$compile');
    $rootScope = $injector.get('$rootScope');
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
    expect(selectDeviceWidget(0).find('widget-header .header .name').html().trim()).toEqual('device 1');
    expect(selectDeviceWidget(1).find('widget-header .header .name').html().trim()).toEqual('device 2');

    $rootScope.page.items.push({
      name: 'device 3'
    });
    $rootScope.$digest();

    expect(selectDeviceWidgets().length).toEqual(3);
    expect(selectDeviceWidget(0).find('widget-header .header .name').html().trim()).toEqual('device 1');
    expect(selectDeviceWidget(1).find('widget-header .header .name').html().trim()).toEqual('device 2');
    expect(selectDeviceWidget(2).find('widget-header .header .name').html().trim()).toEqual('device 3');
  });

  it('includes locations', function() {
    $rootScope.$digest();

    expect(selectDeviceWidgets().length).toEqual(0);

    $rootScope.page.items.push({
      name: 'device 1',
      location: {
        name: 'a'
      }
    });

    $rootScope.page.items.push({
      name: 'device 2',
      location: {
        name: 'b/c'
      }
    });

    $rootScope.page.items.push({
      name: 'device 3',
      location: {
        name: 'b/c/d'
      }
    });

    $rootScope.$digest();

    expect(selectDeviceWidgets().length).toEqual(3);
    expect(selectLocationHeaderBlocks().length).toEqual(3);

    window.block = selectLocationHeaderBlock(0);

    expect(selectLocationHeaders(selectLocationHeaderBlock(0)).length).toEqual(1);
    expect(selectLocationHeader(selectLocationHeaderBlock(0), 0).text()).toEqual('a');

    expect(selectLocationHeaders(selectLocationHeaderBlock(1)).length).toEqual(2);
    expect(selectLocationHeader(selectLocationHeaderBlock(1), 0).text()).toEqual('b');
    expect(selectLocationHeader(selectLocationHeaderBlock(1), 1).text()).toEqual('c');

    expect(selectLocationHeaders(selectLocationHeaderBlock(2)).length).toEqual(1);
    expect(selectLocationHeader(selectLocationHeaderBlock(2), 0).text()).toEqual('d');
  });

  it('optionally filters items', function() {
    element = $compile('<device-list devices="page.items", include="include"></device-list>')($rootScope);
    $rootScope.$digest();

    expect(selectDeviceWidgets().length).toEqual(0);

    $rootScope.page.items.push({
      name: 'device 1'
    });
    $rootScope.page.items.push({
      name: 'device 2'
    });
    $rootScope.include = function(device) {
      return device.name !== 'device 2';
    };
    $rootScope.$digest();

    //TODO test locations also
    expect(selectDeviceWidgets().length).toEqual(1);
    expect(selectDeviceWidget(0).find('widget-header .header .name').html().trim()).toEqual('device 1');

    $rootScope.page.items.push({
      name: 'device 3'
    });
    $rootScope.$digest();

    expect(selectDeviceWidgets().length).toEqual(2);
    expect(selectDeviceWidget(0).find('widget-header .header .name').html().trim()).toEqual('device 1');
    expect(selectDeviceWidget(1).find('widget-header .header .name').html().trim()).toEqual('device 3');
  });

  function selectDeviceWidgets() {
    return $(element).find('device-widget');
  }

  function selectDeviceWidget(index) {
    return selectDeviceWidgets().eq(index);
  }

  function selectLocationHeaderBlocks() {
    return $(element).find('location-header-group');
  }

  function selectLocationHeaderBlock(index) {
    return selectLocationHeaderBlocks().eq(index);
  }

  function selectLocationHeaders(locationHeaderBlock) {
    return locationHeaderBlock.children();
  }

  function selectLocationHeader(locationHeaderBlock, index) {
    return selectLocationHeaders(locationHeaderBlock).eq(index).children();
  }
});
