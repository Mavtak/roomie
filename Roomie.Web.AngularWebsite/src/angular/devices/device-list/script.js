function deviceList() {

  return {
    restrict: 'E',
    scope: {
      devices: '=devices',
      include: '=include'
    },
    link: link,
    templateUrl: 'devices/device-list/template.html',
  };

  function link(scope) {
    scope.$watch('devices', filterDevices, true);

    filterDevices();

    function filterDevices() {
      if (typeof scope.include === 'undefined') {
        scope.filteredDevices = scope.devices;

        return;
      }

      scope.filteredDevices = _.filter(scope.devices, scope.include);
    }
  }

}

export default deviceList;
