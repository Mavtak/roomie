function dock() {

  return {
    transclude: true,
    restrict: 'E',
    scope: {
      area: '@area',
      pixelHeight: '=?pixelHeight'
    },
    link: link,
    templateUrl: 'common/dock/template.html',
  };

  function link(scope, element) {
    var content = element.contents()[0];

    scope.fillerStyle = {};

    scope.$watch(calculateHeight, updateHeight);

    function calculateHeight() {
      return content.offsetHeight;
    }

    function updateHeight(newValue) {
      scope.fillerStyle.height = newValue + 'px';
      scope.pixelHeight = newValue;
    }
  }

}

export default dock;
