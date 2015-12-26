angular.module('roomie.common').directive('appContentLoadingIndicator', function (wholePageStatus) {

  return {
    restrict: 'E',
    link: link,
    templateUrl: 'common/app-content-loading-indicator/template.html',
  };

  function link(scope) {
    scope.wholePageStatus = wholePageStatus;
  }  
});
