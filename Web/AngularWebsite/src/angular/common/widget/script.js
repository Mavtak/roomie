var module = angular.module('roomie.common');

module.directive('widget', function() {
  return {
    restrict: 'E',
    transclude: true,
    template: '<div class="widget"><div class="content" ng-transclude></div></div>'
  };
});
