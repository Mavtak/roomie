angular.module('roomie.users').controller('SignInController', function (
  $http,
  $scope,
  $state,
  wholePageStatus
  ) {
  $scope.username = '';
  $scope.password = '';
  $scope.submit = submit;
  wholePageStatus.set('ready');

  function submit() {
    wholePageStatus.set('loading');

    var path = '/api/UserAuthentication?username=' + encodeURIComponent($scope.username) + '&password=' + encodeURIComponent($scope.password);
    $http.post(path).then(function() {
      $state.go('devices');
    });
  }
});
