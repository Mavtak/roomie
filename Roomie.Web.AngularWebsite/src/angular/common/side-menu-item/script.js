import template from './template.html';

function sideMenuItem() {

  return {
    restrict: 'E',
    scope: {
      indent: '=indent',
      label: '=label',
      selected: '=selected',
      target: '=target'
    },
    link: link,
    template: template,
  };

  function link(scope) {
    scope.calculateIndent = calculateIndent;

    function calculateIndent() {
      var result = '';
      var spaces = (scope.indent || 0) * 2;

      for (var i = 0; i < spaces; i++) {
        result += '\xA0';
      }

      return result;
    }
  }

}

export default sideMenuItem;
