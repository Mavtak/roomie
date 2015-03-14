var module = angular.module('roomie.devices');

module.config(['$stateProvider', function ($stateProvider) {
  $stateProvider.state('devices', {
    url: '/devices',
    controller: 'DevicesController',
    template: '<device-widget device="device" ng-repeat="device in page.items"></device-widget>'
  });

  $stateProvider.state('device detail', {
    url: '/devices/:id',
    controller: 'DevicesController',
    template: '' +
      '<device-widget ' +
        'ng-if="page.items[0]" ' +
        'device="page.items[0]"' +
        '>' +
      '</device-widget>'
  });
}]);
