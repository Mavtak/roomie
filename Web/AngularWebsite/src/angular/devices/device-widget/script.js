angular.module('roomie.devices').directive('deviceWidget', function () {
  return {
    restrict: 'E',
    scope: {
      device: '=device',
      showEdit: '=showEdit'
    },
    templateUrl: 'devices/device-widget/template.html',
  };
});
