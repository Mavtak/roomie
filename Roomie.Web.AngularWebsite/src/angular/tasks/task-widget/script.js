import template from './template.html';

function taskWidget() {

  return {
    restrict: 'E',
    scope: {
      task: '=task'
    },
    template: template,
  };

}

export default taskWidget;
