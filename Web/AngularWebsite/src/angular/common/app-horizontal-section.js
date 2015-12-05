var module = angular.module('roomie.common');

module.directive('appHorizontalSection', function () {

  return {
    restrict: 'E',
    transclude: true,
    scope: {
      rowId: '@rowId'
    },
    template: '' +
      '<div ' +
        'id="{{rowId}}" ' +
        'class="horizontalCrossSection"' +
        '>' +
        '<div ' +
          'class="mainColumn"' +
          'ng-transclude' +
          '>' +
        '</div>' +
      '</div>'
  };

});
