angular.module('roomie.users').controller('SignOutController', function (
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
    $state.go('help');
  }

  function handleErrorResponse(response) {
    wholePageStatus.set('ready');
    $state.go('help');
  }

  function submit() {
    var path = '/api/UserAuthentication';
    $http.delete(path).then(handleSuccessResponse, handleErrorResponse);
  }
});
