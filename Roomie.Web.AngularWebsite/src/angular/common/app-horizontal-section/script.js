import template from './template.html';

function appHorizontalSection() {

  return {
    restrict: 'E',
    transclude: true,
    scope: {
      rowId: '@rowId'
    },
    template: template,
  };

}

export default appHorizontalSection;
