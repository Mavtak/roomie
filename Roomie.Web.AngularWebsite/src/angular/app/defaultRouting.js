function routing(
  $stateProvider
) {

  $stateProvider.state('default', {
    url: '',
    controller: function (
      $http,
      $state,
      signInState,
      wholePageStatus
    ) {
      wholePageStatus.set('loading');

      $http.get('/api/UserAuthentication')
        .then(function () {
          wholePageStatus.set('ready');
          signInState.set('signed-in');
          $state.go('devices');
        }, function () {
          wholePageStatus.set('ready');
          signInState.set('signed-out');
          $state.go('help/about');
        });
    }
  });

};

export default routing;
