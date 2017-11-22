﻿function routing(
  $stateProvider
) {

  $stateProvider.state('devices', {
    url: '/devices?examples&location',
    controller: 'DevicesController',
    controllerAs: 'controller',
    templateUrl: 'devices.pages/index.html'
  });

  $stateProvider.state('device detail', {
    url: '/devices/:id',
    controller: 'DevicesController',
    controllerAs: 'controller',
    templateUrl: 'devices.pages/detail.html'
  });

}

export default routing;
