angular.module('roomie.help').config(function (
  $stateProvider
  ) {
  $stateProvider.state('help/about', {
    url: '/help/about',
    templateUrl: 'help/pages/about.html'
  });
});
