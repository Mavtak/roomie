function binarySwitchControls() {

  return {
    restrict: 'E',
    scope: {
      binarySwitch: '=binarySwitch',
    },
    templateUrl: 'devices/binary-switch-controls/template.html',
  };

}

export default binarySwitchControls;
