import template from './template.html';

function computerWidget() {

  return {
    restrict: 'E',
    scope: {
      computer: '=computer',
      showEdit: '=showEdit'
    },
    template: template,
  };

}

export default computerWidget;
