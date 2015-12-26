angular.module('roomie.common').directive('widgetButton', function () {
  return {
    restrict: 'E',
    scope: {
      activate: '&activate',
      activated: '=activated',
      label: "@label",
      color: "@color"
    },
    link: link,
    templateUrl: 'common/widget-button/template.html',
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
