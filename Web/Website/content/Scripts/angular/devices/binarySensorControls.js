var module = angular.module('roomie.devices');

module.directive('binarySensorControls', function () {

  return {
    restrict: 'E',
    scope: {
      label: '@label',
      sensor: '=sensor'
    },
    link: link,
    template: '' +
      '<div ' +
        'class="group" ' +
        '>' +
        '<button ' +
          'ng-click="sensor.poll()"' +
          '>' +
          '{{translateLabel(sensor)}}: {{translateValue(sensor)}} (at {{sensor.timeStamp.toLocaleString()}})' +
        '</button>' +
      '</div>'
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

});
