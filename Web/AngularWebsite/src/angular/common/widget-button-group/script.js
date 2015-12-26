var module = angular.module('roomie.common');

module.directive('widgetButtonGroup', function() {
  return {
    restrict: 'E',
    transclude: true,
    templateUrl: 'common/widget-button-group/template.html',
  };
});
