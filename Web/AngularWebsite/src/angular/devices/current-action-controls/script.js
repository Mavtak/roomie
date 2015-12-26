var module = angular.module('roomie.devices');

module.directive('currentActionControls', function() {

  return {
    restrict: 'E',
    scope: {
      currentAction: '=currentAction'
    },
    template: '' +
      '<div ' +
        'class="group" ' +
        '>' +
        '<div ' +
          '>' +
          '{{currentAction}}' +
        '</div>' +
      '</div>'
  };

});
