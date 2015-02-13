var module = angular.module('roomie.common');

module.directive('keyValue', function() {
  return {
    restrict: 'E',
    scope: {
      key: '@key',
      value: '@value',
      href: '@href'
    },
    template: '<div class="item"><div class="key">{{key}}:</div><a ng-href="{{href}}" class="value">{{value}}</a></div>'
  };
});
