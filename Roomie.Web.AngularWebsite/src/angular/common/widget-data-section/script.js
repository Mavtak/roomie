import template from './template.html';

function widgetDataSection() {

  return {
    restrict: 'E',
    transclude: true,
    template: template,
  };

}

export default widgetDataSection;
