angular.module('roomie.app').config(function (
  $stateProvider,
  $urlRouterProvider
) {
  $stateProvider.state('not found', {
    templateUrl: 'not-found.html',
  });

  $urlRouterProvider.otherwise(function ($injector) {
    var $state = $injector.get('$state');

    $state.go('not found', null, {
      location: false,
    });
  });

});
