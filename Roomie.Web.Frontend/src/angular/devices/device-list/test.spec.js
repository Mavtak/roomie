describe('angular roomie.devices device-list (directive)', function () {
  var $injector;
  var $scope;

  beforeEach(angular.mock.module('roomie.devices'));

  beforeEach(angular.mock.inject(function (_$injector_) {
    $injector = _$injector_;
    $scope = $injector.get('$rootScope').$new();
  }));

  beforeEach(function () {
    $scope.page = {
      items: []
    };
  });

  it('lists out the items', function () {
    var element = compileDirective('<device-list devices="page.items"></device-list>');

    expect(selectDeviceWidgets(element).length).toEqual(0);

    $scope.page.items.push({
      name: 'device 1'
    });
    $scope.page.items.push({
      name: 'device 2'
    });
    $scope.$digest();

    expect(selectDeviceWidgets(element).length).toEqual(2);
    expect(selectDeviceWidget(element, 0).find('widget-header .header .name').html().trim()).toEqual('device 1');
    expect(selectDeviceWidget(element, 1).find('widget-header .header .name').html().trim()).toEqual('device 2');

    $scope.page.items.push({
      name: 'device 3'
    });
    $scope.$digest();

    expect(selectDeviceWidgets(element).length).toEqual(3);
    expect(selectDeviceWidget(element, 0).find('widget-header .header .name').html().trim()).toEqual('device 1');
    expect(selectDeviceWidget(element, 1).find('widget-header .header .name').html().trim()).toEqual('device 2');
    expect(selectDeviceWidget(element, 2).find('widget-header .header .name').html().trim()).toEqual('device 3');
  });

  it('includes locations', function () {
    var element = compileDirective('<device-list devices="page.items"></device-list>');

    expect(selectDeviceWidgets().length).toEqual(0);

    $scope.page.items.push({
      name: 'device 1',
      location: {
        name: 'a'
      }
    });

    $scope.page.items.push({
      name: 'device 2',
      location: {
        name: 'b/c'
      }
    });

    $scope.page.items.push({
      name: 'device 3',
      location: {
        name: 'b/c/d'
      }
    });

    $scope.$digest();

    expect(selectDeviceWidgets(element).length).toEqual(3);
    expect(selectLocationHeaderBlocks(element).length).toEqual(3);

    window.block = selectLocationHeaderBlock(element, 0);

    expect(selectLocationHeaders(selectLocationHeaderBlock(element, 0)).length).toEqual(1);
    expect(selectLocationHeader(selectLocationHeaderBlock(element, 0), 0).text()).toEqual('a');

    expect(selectLocationHeaders(selectLocationHeaderBlock(element, 1)).length).toEqual(2);
    expect(selectLocationHeader(selectLocationHeaderBlock(element, 1), 0).text()).toEqual('b');
    expect(selectLocationHeader(selectLocationHeaderBlock(element, 1), 1).text()).toEqual('c');

    expect(selectLocationHeaders(selectLocationHeaderBlock(element, 2)).length).toEqual(1);
    expect(selectLocationHeader(selectLocationHeaderBlock(element, 2), 0).text()).toEqual('d');
  });

  it('optionally filters items', function () {
    var element = compileDirective('<device-list devices="page.items", include="include"></device-list>');

    expect(selectDeviceWidgets(element).length).toEqual(0);

    $scope.page.items.push({
      name: 'device 1'
    });
    $scope.page.items.push({
      name: 'device 2'
    });
    $scope.include = function (device) {
      return device.name !== 'device 2';
    };
    $scope.$digest();

    //TODO test locations also
    expect(selectDeviceWidgets(element).length).toEqual(1);
    expect(selectDeviceWidget(element, 0).find('widget-header .header .name').html().trim()).toEqual('device 1');

    $scope.page.items.push({
      name: 'device 3'
    });
    $scope.$digest();

    expect(selectDeviceWidgets(element).length).toEqual(2);
    expect(selectDeviceWidget(element, 0).find('widget-header .header .name').html().trim()).toEqual('device 1');
    expect(selectDeviceWidget(element, 1).find('widget-header .header .name').html().trim()).toEqual('device 3');
  });

  function compileDirective(html) {
    var $compile = $injector.get('$compile');
    var element = $compile(html)($scope);
    $scope.$digest();

    return element;
  }

  function selectDeviceWidgets(element) {
    return $(element).find('device-widget');
  }

  function selectDeviceWidget(element, index) {
    return selectDeviceWidgets(element, index).eq(index);
  }

  function selectLocationHeaderBlocks(element) {
    return $(element).find('location-header-group');
  }

  function selectLocationHeaderBlock(element, index) {
    return selectLocationHeaderBlocks(element).eq(index);
  }

  function selectLocationHeaders(locationHeaderBlock) {
    return locationHeaderBlock.children();
  }

  function selectLocationHeader(locationHeaderBlock, index) {
    return selectLocationHeaders(locationHeaderBlock).eq(index).children();
  }

});
