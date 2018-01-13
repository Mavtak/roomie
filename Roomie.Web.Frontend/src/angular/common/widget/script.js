import template from './template.html';

function widget() {

  return {
    restrict: 'E',
    transclude: true,
    template: template,
  };

};

export default widget;
