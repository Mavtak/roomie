angular.module('roomie.common');

module.directive('appContent', function () {

  return {
    restrict: 'E',
    replace: true,
    template: '' +
      '<div ' +
        'id="content" ' +
        'ui-view' +
        '>' +
      '</div>'
  };

});
