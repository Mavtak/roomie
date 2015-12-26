var module = angular.module('roomie.devices');

module.directive('currentActionControls', function() {

  return {
    restrict: 'E',
    scope: {
      currentAction: '=currentAction'
    },
    templateUrl: 'devices/current-action-controls/template.html',
  };

});
