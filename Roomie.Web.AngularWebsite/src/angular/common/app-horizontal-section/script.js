angular.module('roomie.common').directive('appHorizontalSection', function () {

  return {
    restrict: 'E',
    transclude: true,
    scope: {
      rowId: '@rowId'
    },
    templateUrl: 'common/app-horizontal-section/template.html',
  };

});
