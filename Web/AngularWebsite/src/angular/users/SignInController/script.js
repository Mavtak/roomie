angular.module('roomie.users').controller('SignInController', function (
  $http,
  $scope,
  $state,
  wholePageStatus
  ) {
  var self = this;
  this.username = '';
  this.password = '';
  this.submit = submit;
  wholePageStatus.set('ready');
  resetError();

  function handleSuccessResponse() {
    $state.go('devices');
  }

  function handleErrorResponse(response) {
    self.errors = _.map(response.data, function (x) {
      return x.friendlyMessage;
    });
    wholePageStatus.set('ready');
  }

  function resetError() {
    self.errors = [];
  }

  function submit() {
    wholePageStatus.set('loading');
    resetError();

    var path = '/api/UserAuthentication?username=' + encodeURIComponent(this.username) + '&password=' + encodeURIComponent(this.password);
    $http.post(path).then(handleSuccessResponse, handleErrorResponse);
  }
});
