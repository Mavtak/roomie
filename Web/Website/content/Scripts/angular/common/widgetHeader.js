var module = angular.module('roomie.common');

module.directive('widgetHeader', function() {
  return {
    restrict: 'E',
    scope: {
      disconnected: '=disconnected',
      title: '@title',
      subtitle: '@subtitle',
      href: '@href'
    },
    template: '' +
      '<a ' +
        'class="header" ' +
        'ng-href="{{href}}"' +
        '>' +
        '<div ' +
          'class="location"' +
          '>' +
          '{{subtitle}}' +
        '</div>' +
        '<widget-disconnected-icon ' +
          'ng-if="disconnected"' +
          '>' +
        '</widget-disconnected-icon>' +
        '<div ' +
          'class="name"' +
          '>' +
          '{{title}}' +
        '</div>' +
      '</a>'
  };
});
