function appHorizontalSection() {

  return {
    restrict: 'E',
    transclude: true,
    scope: {
      rowId: '@rowId'
    },
    templateUrl: 'common/app-horizontal-section/template.html',
  };

}

export default appHorizontalSection;
