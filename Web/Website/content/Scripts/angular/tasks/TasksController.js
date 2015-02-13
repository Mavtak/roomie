var module = angular.module('roomie.tasks');

module.controller('TasksController', ['$http', '$scope', function ($http, $scope) {

  var start = 0;
  var count = 10;
  
  if (typeof $scope.$state.params.start !== 'undefined') {
    start = $scope.$state.params.start;
  }
  
  if (typeof $scope.$state.params.count !== "undefined") {
    count = $scope.$state.params.count;
  }

  $http.get('/api/task?start=' + start + '&count=' + count).success(function(page) {
    $scope.page = page;
    for (var i = 0; i < $scope.page.items.length; i++) {
      processTask($scope.page.items[i]);
    }
  });

  function processTask(task) {
    if (task.expiration) {
      task.expiration = new Date(task.expiration);
    }

    if (task.receivedTimestamp) {
      task.receivedTimestamp = new Date(task.receivedTimestamp);
    }

    if (task.script) {
      if (task.script.creationTimestamp) {
        task.script.creationTimestamp = new Date(task.script.creationTimestamp);
      }
    }
  }
}]);
