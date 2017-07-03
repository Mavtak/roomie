angular.module('roomie.common').directive('keyValue', function () {

  return {
    restrict: 'E',
    scope: {
      key: '@key',
      value: '@value',
      href: '@href',
      fullWidth: '=fullWidth'
    },
    templateUrl: 'common/key-value/template.html',
  };

});
