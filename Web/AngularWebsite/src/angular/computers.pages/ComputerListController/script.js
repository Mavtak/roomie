angular.module('roomie.computers.pages').controller('ComputerListController', function (
  $http,
  pageMenuItems,
  signInState,
  wholePageStatus
) {

  var controller = this;

  wholePageStatus.set('loading');
  pageMenuItems.reset();

  $http.get('/api/computer/')
    .then(function (result) {
      controller.computers = result.data;

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