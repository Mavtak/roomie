function currentActionControls() {

  return {
    restrict: 'E',
    scope: {
      currentAction: '=currentAction'
    },
    templateUrl: 'devices/current-action-controls/template.html',
  };

}

export default currentActionControls;
