var module = angular.module('roomie.common');

module.directive('roomieApp', function() {

  return {
    restrict: 'E',
    template: '' +
      '<div id="page">' +
        '<div ' +
          'id="headerRow" ' +
          'class="horizontalCrossSection"' +
          '>' +
          '<app-header' +
            '>' +
          '</app-header>' +
        '</div>' +
        '<div ' +
          'id="contentRow" ' +
          'class="horizontalCrossSection"' +
          '>' +
          '<app-content' +
            '>' +
          '</app-content>' +
        '</div>' +
        '<div ' +
          'id="footerRow" ' +
          'class="horizontalCrossSection"' +
          '>' +
          '<app-footer' +
            '>' +
          '</app-footer>' +
        '</div>' +
      '</div>'
  };

});
