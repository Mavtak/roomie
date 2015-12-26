var module = angular.module('roomie.common');

module.directive('widget', function() {
  return {
    restrict: 'E',
    transclude: true,
    templateUrl: 'common/widget/template.html',
  };
});
