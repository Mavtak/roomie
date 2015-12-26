var module = angular.module('roomie.common');

module.directive('sideMenuSet', function() {

  return {
    restrict: 'E',
    transclude: true,
    scope: {
      bottom: '=bottom',
      top: '=top',
      width: '=width'
    },
    link: link,
    template: '' +
      '<div ' +
        'ng-style="style" ' +
        'ng-transclude' +
        '>' +
      '</div>'
  };

  function link(scope) {
    scope.style = {
      position: 'fixed'
    };

    scope.$watch('top', updateStyle);
    scope.$watch('bottom', updateStyle);
    scope.$watch('width', updateStyle);

    updateStyle();

    function updateStyle() {
      scope.style.top = scope.top;
      scope.style.bottom = scope.bottom;
      scope.style.width = scope.width;
    }
  }

});
