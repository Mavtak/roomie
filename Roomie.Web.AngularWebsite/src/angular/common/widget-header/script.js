import template from './template.html';

function widgetHeader() {

  return {
    restrict: 'E',
    scope: {
      disconnected: '=disconnected',
      title: '@title',
      subtitle: '@subtitle',
      href: '@href'
    },
    template: template,
  };

};

export default widgetHeader;
