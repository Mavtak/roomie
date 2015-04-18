var module = angular.module('roomie.common');

module.directive('appFooter', function () {

  return {
    restrict: 'E',
    template: '' +
      '<div id="footer">' +
        '<a href="http://davidmcgrath.com/roomie">Roomie</a> is an <a href="/source">open source</a> project by <a href="http://davidmcgrath.com">David McGrath</a>.' +
      '</div>'
  };

});
