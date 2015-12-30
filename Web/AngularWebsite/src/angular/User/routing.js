angular.module('roomie.users').config(function (
  $stateProvider
  ) {
  $stateProvider.state('sign-in', {
    url: '/sign-in',
    controller: 'SignInController',
    templateUrl: 'User/pages/sign-in.html'
  });
});
