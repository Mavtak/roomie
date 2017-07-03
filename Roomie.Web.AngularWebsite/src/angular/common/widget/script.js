angular.module('roomie.common').directive('widget', function () {

  return {
    restrict: 'E',
    transclude: true,
    templateUrl: 'common/widget/template.html',
  };

});
