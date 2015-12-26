var module = angular.module('roomie.common');

module.directive('widgetDisconnectedIcon', function() {

  return {
    restrict: 'E',
    templateUrl: 'common/widget-disconnected-icon/template.html',
  };

});
