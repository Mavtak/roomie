var module = angular.module('roomie.devices');

module.directive('locationHeaderGroup', ['LocationHeaderLabelGenerator', function(LocationHeaderLabelGenerator) {

  return {
    restrict: 'E',
    scope: {
      currentLocation: '=currentLocation',
      previousLocation: '=previousLocation'
    },
    link: link,
    template: '' +
      '<div ' +
        'ng-repeat="part in parts"' +
        '>' +
        '<h2 ng-if="part.depth === 0">{{part.label}}</h2>' +
        '<h3 ng-if="part.depth === 1">{{part.label}}</h3>' +
        '<h4 ng-if="part.depth === 2">{{part.label}}</h4>' +
        '<h5 ng-if="part.depth === 3">{{part.label}}</h5>' +
        '<h6 ng-if="part.depth > 3">{{part.label}}</h6>' +
      '</div>'
  };

  function link(scope) {
    var labelGenerator = new LocationHeaderLabelGenerator(scope.previousLocation, scope.currentLocation);
    scope.parts = labelGenerator.getParts();
  }
}]);
