angular.module('roomie.users').controller('SignInController', function (
  $http,
  $scope,
  $state,
  wholePageStatus
  ) {
  this.username = '';
  this.password = '';
  this.submit = submit;
  wholePageStatus.set('ready');

  function submit() {
    wholePageStatus.set('loading');

    var path = '/api/UserAuthentication?username=' + encodeURIComponent(this.username) + '&password=' + encodeURIComponent(this.password);
    $http.post(path).then(function() {
      $state.go('devices');
    });
  }
});
