﻿var module = angular.module('roomie.tasks');

module.config(['$stateProvider', function ($stateProvider) {
  $stateProvider.state('tasks', {
    url: '/tasks?start&count',
    controller: 'TasksController',
    template: '<task-widget task="task" ng-repeat="task in page.items"></task-widget>'
  });
}]);