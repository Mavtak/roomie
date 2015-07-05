var module = angular.module('roomie.common');

module.directive('sideMenuItem', function() {

  return {
    restrict: 'E',
    scope: {
      label: '=label',
      selected: '=selected',
      target: '=target'
    },
    link: link,
    template: '' +
      '<a ' +
        'class="item"' +
        'href="{{target}}"' +
        'ng-click="selected()"' +
        '>' +
        '<span ' +
          'class="content"' +
          '>' +
          '{{label}}' +
        '</span>' +
      '</a>'
  };

});
