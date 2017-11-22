import template from './template.html';

function widgetButtonGroup() {

  return {
    restrict: 'E',
    transclude: true,
    template: template,
  };

}

export default widgetButtonGroup;
