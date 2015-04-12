angular.module('roomie.common');

module.directive('appContent', function () {

  return {
    restrict: 'E',
    template: '' +
      '<div ' +
        'id="content" ' +
        'ui-view' +
        '>' +
      '</div>'
  };

});
