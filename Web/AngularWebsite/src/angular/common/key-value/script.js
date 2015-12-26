var module = angular.module('roomie.common');

module.directive('keyValue', function() {
  return {
    restrict: 'E',
    scope: {
      key: '@key',
      value: '@value',
      href: '@href'
    },
    templateUrl: 'common/key-value/template.html',
  };
});
