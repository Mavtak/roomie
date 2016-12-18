angular.module('roomie.computers').directive('computerWidget', function () {

  return {
    restrict: 'E',
    scope: {
      computer: '=computer'
    },
    templateUrl: 'computers/computer-widget/template.html'
  };

});
