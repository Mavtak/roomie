angular.module('roomie.help').config(function (
  $stateProvider
  ) {
  $stateProvider.state('help', {
    url: '/help',
    templateUrl: 'help/pages/index.html'
  });

  $stateProvider.state('help/about', {
    url: '/help/about',
    templateUrl: 'help/pages/about.html'
  });

  $stateProvider.state('help/hardware', {
    url: '/help/hardware',
    templateUrl: 'help/pages/hardware.html'
  });
});
