var module = angular.module('roomie.common');

module.directive('sideMenuItem', function() {

  return {
    restrict: 'E',
    scope: {
      indent: '=indent',
      label: '=label',
      selected: '=selected',
      target: '=target'
    },
    link: link,
    template: '' +
      '<a ' +
        'class="item"' +
        'href="{{target}}"' +
        'ng-click="selected()"' +
        '>' +
        '{{calculateIndent()}}' +
        '<span ' +
          'class="content"' +
          '>' +
          '{{label}}' +
        '</span>' +
      '</a>'
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
});
