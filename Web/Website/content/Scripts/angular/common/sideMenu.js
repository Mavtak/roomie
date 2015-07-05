var module = angular.module('roomie.common');

module.directive('sideMenu', ['$window', function($window) {

  return {
    restrict: 'E',
    transclude: true,
    scope: {
      calculatedWidth: '=calculatedWidth',
      itemSelected: '&itemSelected'
    },
    link: link,
    template: '' +
      '<div ' +
        'class="sideMenu" ' +
        'ng-style="style" ' +
        'ng-transclude' +
        '>' +
      '</div>'
  };
  
  function link(scope, element, attributes) {
    scope.style = {
      width: 'inherit'
    };

    if (attributes.hasOwnProperty('calculatedWidth')) {
      scope.$watch(calculateWidth, updateWidth);
    }

    function calculateWidth() {
      var sideMenu = element.contents()[0];
      var result = sideMenu.offsetWidth;

      if (result === 0 && typeof scope.calculatedWidth !== 'undefined') {
        result = scope.calculatedWidth;
      } else {
        result += 'px';
      }

      return result;
    }
    
    function updateWidth(newValue) {
      scope.calculatedWidth = newValue;
      console.log(newValue);
    }
  }

}]);
