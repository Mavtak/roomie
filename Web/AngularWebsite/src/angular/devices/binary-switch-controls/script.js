angular.module('roomie.devices').directive('binarySwitchControls', function () {

  return {
    restrict: 'E',
    scope: {
      binarySwitch: '=binarySwitch',
    },
    templateUrl: 'devices/binary-switch-controls/template.html',
  };

});
