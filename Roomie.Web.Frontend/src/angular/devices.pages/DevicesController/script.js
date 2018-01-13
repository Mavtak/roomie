function DevicesController(
  $scope,
  $state,
  AutomaticPollingDeviceListing,
  devices,
  locationUtilities,
  pageMenuItems,
  signInState,
  wholePageStatus
) {

  var controller = this;

  if ($state.params.examples === 'true') {
    setUpExamples();
  }

  if (devices.ready === false) {
    showLoading();
  }

  pageMenuItems.reset();
  controller.include = shouldShowDevice;
  if (typeof $state.params.id !== 'undefined') {
    setPageBySingleItem();
  } else {
    setPageByCompleteListing();
  }
  keepPageMenuItemsUpdated();

  function keepPageMenuItemsUpdated() {
    $scope.$watchCollection(read, update, true);

    function read() {
      return locationUtilities.extractFromDevices(devices.page.items);
    }

    function update(locations) {
      var items = locationUtilities.calculatePageMenuItems(locations);

      pageMenuItems.set(items);
    }
  }

  function setPageByCompleteListing() {
    Object.defineProperty(controller, 'page', {
      get: function () { return devices.page; }
    });
  }

  function setPageBySingleItem() {
    var page = {
      items: []
    };

    Object.defineProperty(controller, 'page', {
      get: function () { return page; }
    });

    var stopWatching = $scope.$watchCollection(read, update, true);

    function read() {
      return devices.ready;
    }


    function update(newValue, oldValue) {
      if (newValue !== true) {
        return;
      }

      var match = _.find(devices.page.items, {id: +$state.params.id});

      if (typeof match === 'undefined') {
        return;
      }

      page.items.push(match);

      stopWatching();
    }
  }

  function setUpExamples() {
    var examples = new AutomaticPollingDeviceListing({
      examples: true
    });

    examples.run();

    $scope.$on('$destroy', function () {
      examples.stop();
    });

    devices = examples;
  }

  function shouldShowDevice(device) {
    var location = $state.params.location;

    if (typeof location !== 'undefined' && location !== '') {
      if (typeof device.location === 'undefined') {
        return false;
      }

      if (typeof device.location.name !== 'string') {
        return false;
      }

      if (device.location.name.indexOf(location) !== 0 && location.indexOf(device.location.name) !== 0) {
        return false;
      }
    }

    return true;
  }

  function showLoading() {
    wholePageStatus.set('loading');

    var stopWatching = $scope.$watch(read, update);

    function read() {
      return devices.ready;
    }

    function update(newValue, oldValue) {
      if (newValue === oldValue || newValue !== true) {
        return;
      }

      wholePageStatus.set('ready');
      stopWatching();
    }
  }

}

export default DevicesController;
