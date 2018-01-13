import notFoundTemplate from './not-found.html';

function routing(
  $stateProvider,
  $urlRouterProvider
) {
  $stateProvider.state('not found', {
    template: notFoundTemplate,
  });

  $urlRouterProvider.otherwise(function ($injector) {
    var $state = $injector.get('$state');

    $state.go('not found', null, {
      location: false,
    });
  });

}

export default routing;
