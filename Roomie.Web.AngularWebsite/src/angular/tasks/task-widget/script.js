function taskWidget() {

  return {
    restrict: 'E',
    scope: {
      task: '=task'
    },
    templateUrl: 'tasks/task-widget/template.html',
  };

}

export default taskWidget;
