var module = angular.module('roomie.common');

module.directive('roomieApp', function() {

  return {
    restrict: 'E',
    template: '' +
      '<div id="page">' +
        '<app-horizontal-section ' +
          'row-id="headerRow" ' +
          '>' +
          '<app-header ' +
            'navigation-menu="navigationMenu" ' +
            '>' +
          '</app-header>' +
        '</app-horizontal-section>' +
        '<app-horizontal-section ' +
          'row-id="contentRow" ' +
          '>' +
          '<app-content' +
            '>' +
          '</app-content>' +
        '</app-horizontal-section>' +
        '<app-horizontal-section ' +
          'row-id="footerRow" ' +
          '>' +
          '<app-footer' +
            '>' +
          '</app-footer>' +
        '</div>' +
      '</app-horizontal-section>'
  };

});
