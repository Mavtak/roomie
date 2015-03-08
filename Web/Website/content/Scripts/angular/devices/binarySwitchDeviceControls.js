var module = angular.module('roomie.devices');

module.directive('binarySwitchDeviceControls', function() {
  return {
    restrict: 'E',
    scope: {
      binarySwitch: '=binarySwitch',
    },
    template: '' +
      '<widget-button-group>' +
        '<widget-button ' +
          'label="Off" ' +
          'activate="binarySwitch.setPower(\'Off\')" ' +
          'activated="binarySwitch.power == \'off\'" ' +
          '>' +
        '</widget-button>' +
        '<widget-button ' +
          'label="On" ' +
          'activate="binarySwitch.setPower(\'On\')" ' +
          'activated="binarySwitch.power == \'on\'" ' +
          '>' +
        '</widget-button>' +
      '</widget-button-group>'
  };
});
