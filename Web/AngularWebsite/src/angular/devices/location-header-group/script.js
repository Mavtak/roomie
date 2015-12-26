var module = angular.module('roomie.devices');

module.directive('locationHeaderGroup', ['LocationHeaderLabelGenerator', function(LocationHeaderLabelGenerator) {

  return {
    restrict: 'E',
    scope: {
      currentLocation: '=currentLocation',
      previousLocation: '=previousLocation'
    },
    link: link,
    templateUrl: 'devices/location-header-group/template.html',
  };

  function link(scope) {
    var labelGenerator = new LocationHeaderLabelGenerator(scope.previousLocation, scope.currentLocation);
    scope.parts = labelGenerator.getParts();
  }
}]);
