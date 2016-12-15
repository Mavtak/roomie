angular.module('roomie.networks').directive('networkWidget', function () {

  return {
    restrict: 'E',
    scope: {
      network: '=network',
    },
    templateUrl: 'networks/network-widget/template.html',
  };

});
