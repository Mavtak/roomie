angular.module('roomie.networks.pages').config(function (
  $stateProvider
) {

  $stateProvider.state('network list', {
    url: '/networks',
    controller: 'NetworkListController',
    controllerAs: 'controller',
    templateUrl: 'networks.pages/index.html',
  });

  $stateProvider.state('network detail', {
    url: '/networks/:id',
    controller: 'NetworkDetailController',
    controllerAs: 'controller',
    templateUrl: 'networks.pages/detail.html',
  });

});
