angular.module('roomie.devices.pages').controller('DevicesController', function (
  $http,
  $scope,
  $state,
  AutomaticPollingDeviceListing,
  locationUtilities,
  pageMenuItems,
  signInState,
  wholePageStatus
) {

  var controller = this;
  var listing = new AutomaticPollingDeviceListing({
    examples: $state.params.examples,
    id: $state.params.id,
  });

  wholePageStatus.set('loading');
  pageMenuItems.reset();
  controller.include = shouldShowDevice;
  Object.defineProperty(controller, 'page', {
    get: function () { return listing.page; }
  });
  keepPageMenuItemsUpdated();

  listing.run();
  $scope.$on('$destroy', function () {
    listing.stop();
  });

  function keepPageMenuItemsUpdated() {
    $scope.$watchCollection(read, update, true);

    function read() {
      return locationUtilities.extractFromDevices(listing.page.items);
    }

    function update(locations) {
      var items = locationUtilities.calculatePageMenuItems(locations);

      pageMenuItems.set(items);
    }
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

});
