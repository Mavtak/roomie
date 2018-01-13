function SignOutController(
  $http,
  $state,
  signInState,
  wholePageStatus
) {

  wholePageStatus.set('loading');

  submit();

  function handleSuccessResponse() {
    wholePageStatus.set('ready');
    signInState.set('signed-out');
    $state.go('help/about');
  }

  function handleErrorResponse(response) {
    wholePageStatus.set('ready');
    $state.go('help/about');
  }

  function submit() {
    var path = '/api/UserAuthentication';
    $http.delete(path).then(handleSuccessResponse, handleErrorResponse);
  }

}

export default SignOutController;
