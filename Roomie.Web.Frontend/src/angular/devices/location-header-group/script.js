import template from './template.html';

function locationHeaderGroup(
  LocationHeaderLabelGenerator
) {

  return {
    restrict: 'E',
    scope: {
      currentLocation: '=currentLocation',
      previousLocation: '=previousLocation'
    },
    link: link,
    template: template,
  };

  function link(scope) {
    var labelGenerator = new LocationHeaderLabelGenerator(scope.previousLocation, scope.currentLocation);
    scope.parts = labelGenerator.getParts();
  }

}

export default locationHeaderGroup;
