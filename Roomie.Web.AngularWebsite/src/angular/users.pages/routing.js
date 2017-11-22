function routing(
  $stateProvider
) {

  $stateProvider.state('sign-in', {
    url: '/sign-in',
    controller: 'SignInController',
    controllerAs: 'controller',
    templateUrl: 'users.pages/sign-in.html'
  });

  $stateProvider.state('sign-out', {
    url: '/sign-out',
    controller: 'SignOutController',
    templateUrl: 'users.pages/sign-out.html'
  });

}

export default routing;
