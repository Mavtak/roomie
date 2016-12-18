angular.module('roomie.computers').directive('addComputerWidget', function (
  $http
) {

  return {
    restrict: 'E',
    scope: {},
    templateUrl: 'computers/add-computer-widget/template.html',
    link: link
  };

  function link(scope) {
    scope.add = add;
    scope.model = {};

    function add() {
      $http.post('/api/computer', scope.model)
    }
  }

});
