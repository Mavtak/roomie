function multilevelSensorControls() {

  return {
    restrict: 'E',
    scope: {
      label: '@label',
      sensor: '=sensor'
    },
    templateUrl: 'devices/multilevel-sensor-controls/template.html',
  };

}

export default multilevelSensorControls;
