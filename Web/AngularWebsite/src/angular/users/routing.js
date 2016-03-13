angular.module('roomie.users').config(function (
  $stateProvider
  ) {
  $stateProvider.state('sign-in', {
    url: '/sign-in',
    controller: 'SignInController',
    controllerAs: 'controller',
    templateUrl: 'users/pages/sign-in.html'
  });
});
