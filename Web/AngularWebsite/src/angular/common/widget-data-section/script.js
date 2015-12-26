var module = angular.module('roomie.common');

module.directive('widgetDataSection', function() {
  return {
    restrict: 'E',
    transclude: true,
    templateUrl: 'common/widget-data-section/template.html',
  };
});
