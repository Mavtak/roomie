angular.module('roomie.tasks').directive('taskWidget', function () {

  return {
    restrict: 'E',
    scope: {
      task: '=task'
    },
    templateUrl: 'tasks/task-widget/template.html',
  };

});
