function binarySensorControls() {

  return {
    restrict: 'E',
    scope: {
      label: '@label',
      sensor: '=sensor'
    },
    link: link,
    templateUrl: 'devices/binary-sensor-controls/template.html',
  };

  function link(scope) {
    scope.translateLabel = translateLabel;
    scope.translateValue = translateValue;
  }

  function translateLabel(sensor) {
    //TODO: account for different values of sensor.type
    return 'Binary Sensor';
  }

  function translateValue(sensor) {
    if (typeof sensor.value === 'undefined') {
      return 'Unknown';
    }

    //TODO: account for different values of sensor.type

    return sensor.value ? 'True' : 'False';
  }

}

export default binarySensorControls;
