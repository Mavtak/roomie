var module = angular.module('roomie.common');

module.directive('widgetHeader', function() {
  return {
    restrict: 'E',
    scope: {
      title: '@title',
      subtitle: '@subtitle',
      href: '@href'
    },
    template: '<a class="header" ng-href="{{href}}"><div class="location">{{subtitle}}</div><div class="name">{{title}}</div></a>'
  };
});
