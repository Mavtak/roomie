angular.module('roomie.common').directive('appHeader', ['pageMenuItems', function (pageMenuItems) {

  return {
    restrict: 'E',
    scope: {
      navigationMenu: '=navigationMenu',
      pageMenu: '=pageMenu',
    },
    link: link,
    templateUrl: 'common/app-header/template.html',
  };

  function link(scope) {
    scope.pageMenuItems = pageMenuItems;
  }
}]);
