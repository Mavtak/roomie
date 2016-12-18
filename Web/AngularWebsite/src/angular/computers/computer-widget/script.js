angular.module('roomie.computers').directive('computerWidget', function () {

  return {
    restrict: 'E',
    scope: {
      computer: '=computer',
      showEdit: '=showEdit'
    },
    templateUrl: 'computers/computer-widget/template.html'
  };

});
