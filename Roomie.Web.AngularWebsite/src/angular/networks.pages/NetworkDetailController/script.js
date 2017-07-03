angular.module('roomie.networks.pages').controller('NetworkDetailController', function (
  $http,
  $state,
  pageMenuItems,
  signInState,
  wholePageStatus
) {

  var controller = this;

  wholePageStatus.set('loading');
  pageMenuItems.reset();

  $http.get('/api/network/' + $state.params.id)
    .then(function (result) {
      controller.network = result.data;

      signInState.set('signed-in');
      wholePageStatus.set('ready');
    }, function (result) {
      var errors = result.data;

      var signInError = _.isArray(errors) && _.some(errors, {
        type: 'must-sign-in'
      });

      if (signInError) {
        signInState.set('signed-out');
      }

      wholePageStatus.set('ready');

      //TODO: handle other errors
    });
});
