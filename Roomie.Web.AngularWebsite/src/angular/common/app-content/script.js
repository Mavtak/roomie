function appContent() {
  return {
    restrict: 'E',
    replace: true,
    link: link,
    templateUrl: 'common/app-content/template.html',
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

}

export default appContent;
