var module = angular.module('roomie.common');

module.directive('widgetButtonGroup', function() {
  return {
    restrict: 'E',
    transclude: true,
    template: '<div class="buttonGroup" ng-transclude></div>'
  };
});
