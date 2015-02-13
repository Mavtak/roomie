var module = angular.module('roomie.common');

module.directive('widgetDataSection', function() {
  return {
    restrict: 'E',
    transclude: true,
    template: '<div class="data" ng-transclude></div>'
  };
});
