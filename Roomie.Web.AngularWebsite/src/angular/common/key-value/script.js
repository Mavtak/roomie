import template from './template.html';

function keyValue() {

  return {
    restrict: 'E',
    scope: {
      key: '@key',
      value: '@value',
      href: '@href',
      fullWidth: '=fullWidth'
    },
    template: template,
  };

};

export default keyValue;
