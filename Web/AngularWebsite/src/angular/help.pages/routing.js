angular.module('roomie.help.pages').config(function (
  $stateProvider
  ) {

  $stateProvider.state('help', {
    url: '/help',
    templateUrl: 'help.pages/index.html'
  });

  $stateProvider.state('help/about', {
    url: '/help/about',
    templateUrl: 'help.pages/about.html'
  });

  $stateProvider.state('/help/command-documentation', {
    url: '/help/command-documentation',
    controller: 'CommandDocumentationController',
    controllerAs: 'ctrl',
    templateUrl: 'help.pages/command-documentation.html'
  });

  $stateProvider.state('help/device-addresses', {
    url: '/help/device-addresses',
    templateUrl: 'help.pages/device-addresses.html'
  });

  $stateProvider.state('help/hardware', {
    url: '/help/hardware',
    templateUrl: 'help.pages/hardware.html'
  });

});
