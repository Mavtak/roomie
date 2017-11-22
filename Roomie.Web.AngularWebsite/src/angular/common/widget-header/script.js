function widgetHeader() {

  return {
    restrict: 'E',
    scope: {
      disconnected: '=disconnected',
      title: '@title',
      subtitle: '@subtitle',
      href: '@href'
    },
    templateUrl: 'common/widget-header/template.html',
  };

};

export default widgetHeader;
