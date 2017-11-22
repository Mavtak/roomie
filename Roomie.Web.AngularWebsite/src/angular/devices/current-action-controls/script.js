import template from './template.html';

function currentActionControls() {

  return {
    restrict: 'E',
    scope: {
      currentAction: '=currentAction'
    },
    template: template,
  };

}

export default currentActionControls;
