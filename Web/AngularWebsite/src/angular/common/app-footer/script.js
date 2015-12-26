var module = angular.module('roomie.common');

module.directive('appFooter', function () {

  return {
    restrict: 'E',
    templateUrl: 'common/app-footer/template.html',
  };

});
