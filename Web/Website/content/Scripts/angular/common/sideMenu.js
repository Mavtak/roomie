var module = angular.module('roomie.common');

module.directive('sideMenu', ['$window', function($window) {

  return {
    restrict: 'E',
    scope: {
      itemSelected: '&itemSelected'
    },
    link: link,
    template: '' +
      '<div ' +
        'class="sideMenu" ' +
        'ng-style="style"' +
        '>' +
        '<a class="item" href="#devices" ng-click="itemSelected()"><span class="content">Devices</span></a>' +
        '<a class="item" href="#tasks" ng-click="itemSelected()"><span class="content">Tasks</span></a>' +
      '</div>'
  };
  
  function link(scope) {
    scope.style = {
      left: 'inherit',
      width: 'inherit'
    };
  }

}]);
