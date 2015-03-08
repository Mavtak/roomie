var module = angular.module('roomie.common');

module.directive('widgetButton', function() {
  return {
    restrict: 'E',
    scope: {
      activate: '&activate',
      activated: '=activated',
      label: "@label"
    },
    template: '' +
      '<div class="button">' +
        '<button ' +
          'class="button" ' +
          'ng-click="activate()" ' +
          'ng-class="{activated: activated}"' +
          '>' +
          '{{label}}' +
        '</button>' +
      '</div>'
  };
});
