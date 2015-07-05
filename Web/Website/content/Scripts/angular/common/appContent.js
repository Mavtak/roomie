angular.module('roomie.common');
//TODO: update nested controlers to not rely on root scope
module.directive('appContent', function () {

  return {
    restrict: 'E',
    replace: true,
    link: link,
    template: '' +
      '<div ' +
        'id="content" ' +
        'ui-view' +
        '>' +
      '</div>'
  };

  function link(scope, element) {
    scope.$watch(calculateWidth, updateWidth);

    function calculateWidth() {
      var siblings = element.parent().children();
      var content = siblings[siblings.length - 1];
      
      return content.offsetWidth;
    }

    function updateWidth(newValue) {
      scope.widths.app = newValue;
    }
  }
});
