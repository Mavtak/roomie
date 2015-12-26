angular.module('roomie.common').directive('widgetDataSection', function () {
  return {
    restrict: 'E',
    transclude: true,
    templateUrl: 'common/widget-data-section/template.html',
  };
});
