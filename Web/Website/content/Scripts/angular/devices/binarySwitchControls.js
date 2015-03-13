var module = angular.module('roomie.devices');

module.directive('binarySwitchControls', function() {
  return {
    restrict: 'E',
    scope: {
      binarySwitch: '=binarySwitch',
    },
    template: '' +
      '<widget-button-group>' +
        '<widget-button ' +
          'ng-repeat="power in [\'Off\', \'On\']" ' +
          'label="{{power}}" ' +
          'activate="binarySwitch.setPower(power)" ' +
          'activated="binarySwitch.power == power.toLowerCase()" ' +
          '>' +
        '</widget-button>' +
      '</widget-button-group>'
  };
});
