var module = angular.module('roomie.common');

module.directive('widgetButton', function() {
  return {
    restrict: 'E',
    scope: {
      activate: '&activate',
      activated: '=activated',
      label: "@label",
      color: "@color"
    },
    link: link,
    template: '' +
      '<div class="button">' +
        '<button ' +
          'class="button" ' +
          'ng-click="activate()" ' +
          'ng-class="{activated: activated}" ' +
          'ng-style="style"' +
          '>' +
          '{{label}}' +
        '</button>' +
      '</div>'
  };

  function link(scope) {
    scope.style = {};

    updateColor();

    scope.$watch('color', updateColor);

    function updateColor() {
      scope.style['background-color'] = scope.color;
    }
  }
});
