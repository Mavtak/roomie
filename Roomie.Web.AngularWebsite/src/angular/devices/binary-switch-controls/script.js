import template from './template.html';

function binarySwitchControls() {

  return {
    restrict: 'E',
    scope: {
      binarySwitch: '=binarySwitch',
    },
    template: template,
  };

}

export default binarySwitchControls;
