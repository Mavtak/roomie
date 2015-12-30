angular.module('roomie.devices').config(function (
  $stateProvider
  ) {
  $stateProvider.state('devices', {
    url: '/devices?location',
    controller: 'DevicesController',
    templateUrl: 'devices/pages/index.html'
  });

  $stateProvider.state('device detail', {
    url: '/devices/:id',
    controller: 'DevicesController',
    templateUrl: 'devices/pages/detail.html'
  });
});
