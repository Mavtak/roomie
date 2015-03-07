var module = angular.module('roomie.app');

module.run(['$rootScope', '$state', function ($rootScope, $state) {
  $rootScope.$state = $state;
}]);

module.config(['$stateProvider', function ($stateProvider) {
  $stateProvider.state('devices', {
    url: '/devices',
    controller: 'DevicesController',
    template: '<device-widget device="device" ng-repeat="device in page.items"></device-widget>'
  });

  $stateProvider.state('tasks', {
    url: '/tasks?start&count',
    controller: 'TasksController',
    template: '<task-widget task="task" ng-repeat="task in page.items"></task-widget>'
  });
}]);
