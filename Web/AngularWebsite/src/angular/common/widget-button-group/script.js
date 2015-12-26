angular.module('roomie.common').directive('widgetButtonGroup', function() {
  return {
    restrict: 'E',
    transclude: true,
    templateUrl: 'common/widget-button-group/template.html',
  };
});
