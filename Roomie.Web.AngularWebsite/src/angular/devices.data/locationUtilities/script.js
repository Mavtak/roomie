function locationUtilities(
  LocationHeaderLabelGenerator
) {

  return new LocationUtilities();

  function LocationUtilities() {
    this.calculatePageMenuItems = calculatePageMenuItems;
    this.extractFromDevices = extractFromDevices;

    function calculatePageMenuItems(locations) {
      var previous = '';
      var locationData = _.map(locations, function (current) {
        var generator = new LocationHeaderLabelGenerator(previous, current);
        var parts = generator.getParts();
        previous = current;

        return parts;
      });
      locationData = _.flatten(locationData);

      var result = _.map(locationData, function (item) {
        return {
          indent: item.depth,
          label: item.label,
          target: '#/devices?location=' + item.location
        };
      });

      return result;
    }

    function extractFromDevices(devices) {
      var result = _.map(devices, function (device) {
        if (typeof device.location === 'object' && typeof device.location.name !== 'undefined') {
          return device.location.name;
        }

        return '';
      });

      return result;
    }
  }
}

export default locationUtilities;
