var module = angular.module('roomie.devices');

module.config(['$stateProvider', function ($stateProvider) {
  $stateProvider.state('devices', {
    url: '/devices',
    controller: 'DevicesController',
    template: '<device-widget device="device" ng-repeat="device in page.items"></device-widget>'
  });
}]);
