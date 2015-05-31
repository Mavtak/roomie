var module = angular.module('roomie.common');

module.directive('dock', function() {

  return {
    transclude: true,
    restrict: 'E',
    scope: {
      area: '@area',
      pixelHeight: '=?pixelHeight'
    },
    link: link,
    template: '' +
      '<div ' +
        'class="dock {{area}}" ' +
        'ng-transclude ' +
        '>' +
      '</div>' +
      '<div ' +
        'ng-style="fillerStyle" ' +
        '>' +
      '</div>'
  };
  
  function link(scope, element) {
    var content = element.contents()[0];
    
    scope.fillerStyle = {};

    scope.$watch(calculateHeight, updateHeight);

    function calculateHeight() {
      return content.offsetHeight;
    }
    
    function updateHeight(newValue) {
      scope.fillerStyle.height = newValue + 'px';
      scope.pixelHeight = newValue;
    }
  }

});
