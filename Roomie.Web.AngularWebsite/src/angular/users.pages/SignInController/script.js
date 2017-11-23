function SignInController(
  $http,
  $scope,
  $state,
  signInState,
  wholePageStatus
) {

  var controller = this;

  controller.username = '';
  controller.password = '';
  controller.submit = submit;
  wholePageStatus.set('ready');
  resetError();

  function handleSuccessResponse() {
    signInState.set('signed-in');
    $state.go('devices');
  }

  function handleErrorResponse(response) {
    controller.errors = _.map(response.data, function (x) {
      return x.friendlyMessage;
    });
    wholePageStatus.set('ready');
  }

  function resetError() {
    controller.errors = [];
  }

  function submit() {
    wholePageStatus.set('loading');
    resetError();

    var path = '/api/UserAuthentication?username=' + encodeURIComponent(this.username) + '&password=' + encodeURIComponent(this.password);
    $http.post(path).then(handleSuccessResponse, handleErrorResponse);
  }

}

export default SignInController;
