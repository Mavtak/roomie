angular.module('roomie.tasks.pages').config(function (
  $stateProvider
) {

  $stateProvider.state('tasks', {
    url: '/tasks?start&count',
    controller: 'TasksController',
    templateUrl: 'tasks.pages/index.html',
  });

});
