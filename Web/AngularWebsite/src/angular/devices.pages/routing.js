angular.module('roomie.devices.pages').config(function (
  $stateProvider
) {

  $stateProvider.state('devices', {
    url: '/devices?examples&location',
    controller: 'DevicesController',
    templateUrl: 'devices.pages/index.html'
  });

  $stateProvider.state('device detail', {
    url: '/devices/:id',
    controller: 'DevicesController',
    templateUrl: 'devices.pages/detail.html'
  });

});
