var module = angular.module('roomie.tasks');

module.directive('taskWidget', function () {
  return {
    restrict: 'E',
    scope: {
      task: '=task'
    },
    templateUrl: 'tasks/task-widget/template.html',
  };
});
