﻿var module = angular.module('roomie.app');

module.run(['$rootScope', '$state', function ($rootScope, $state) {
  $rootScope.$state = $state;
}]);

module.config(['$stateProvider', function ($stateProvider) {
  $stateProvider.state('tasks', {
    url: '/tasks?start&count',
    controller: 'TasksController',
    template: '<task-widget task="task" ng-repeat="task in page.items"></task-widget>'
  });
}]);
