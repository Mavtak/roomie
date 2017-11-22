import signInTemplate from './sign-in.html';
import signOutTemplate from './sign-out.html';

function routing(
  $stateProvider
) {

  $stateProvider.state('sign-in', {
    url: '/sign-in',
    controller: 'SignInController',
    controllerAs: 'controller',
    template: signInTemplate,
  });

  $stateProvider.state('sign-out', {
    url: '/sign-out',
    controller: 'SignOutController',
    template: signOutTemplate,
  });

}

export default routing;
