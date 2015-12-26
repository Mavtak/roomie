angular.module('roomie.users').controller('SignInController', ['$http', '$scope', '$state', function ($http, $scope, $state) {
  $scope.username = '';
  $scope.password = '';
  $scope.submit = submit;

  function submit() {
    var path = '/api/UserAuthentication?username=' + encodeURIComponent($scope.username) + '&password=' + encodeURIComponent($scope.password);
    $http.post(path).then(function() {
      $state.go('devices');
    });
  }
}]);
