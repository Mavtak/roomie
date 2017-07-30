angular.module('roomie.common').directive('roomieApp', function (
  $timeout,
  $window,
  signInState,
  pageMenuItems
) {

  return {
    restrict: 'E',
    link: link,
    templateUrl: 'common/roomie-app/template.html',
  };

  function link(scope) {
    scope.contentStyle = {};
    scope.heights = {
      app: 0,
      bottomDock: 0,
      topDock: 0
    };
    scope.widths = {
      app: 0
    };
    scope.navigationMenu = {
      close: closeNavigationMenu,
      isOpen: false,
      open: openNavigationMenu
    };
    scope.pageMenu = {
      close: closePageMenu,
      isOpen: false,
      open: openPageMenu
    };
    scope.pageMenuItems = pageMenuItems;

    scope.navigationMenuItemSelected = closeNavigationMenu;
    scope.pageMenuItemSelected = closePageMenu;

    scope.$watch(calculateHeight, updateHeight);
    scope.$watch('heights', updateContentMinHeight, true);
    scope.$watch('pageMenu.calculatedWidth', function () {
      //TODO: fix it page menu width calculation so that this watch isn't necessary
      if (scope.pageMenu.isOpen) {
        openPageMenu();
      }
    });

    Object.defineProperty(scope, 'signInState', {
      get: function () {
        return signInState.get();
      }
    });

    angular.element($window).bind('resize', function () {
      scope.$apply();
    });

    function calculateHeight() {
      return $window.innerHeight;
    }

    function updateHeight(newValue) {
      scope.heights.app = newValue;
    }

    function openNavigationMenu() {
      scope.contentStyle.left = scope.navigationMenu.calculatedWidth;
      scope.navigationMenu.isOpen = true;

      if (scope.contentStyle.left === '0px') {
        $timeout(openNavigationMenu);
      }
    }

    function openPageMenu() {
      scope.contentStyle.right = scope.pageMenu.calculatedWidth;
      scope.pageMenu.isOpen = true;

      if (scope.contentStyle.right === '0px') {
        $timeout(openPageMenu);
      }
    }

    function closeNavigationMenu() {
      delete scope.contentStyle.left;
      scope.navigationMenu.isOpen = false;
    }

    function closePageMenu() {
      delete scope.contentStyle.right;
      scope.pageMenu.isOpen = false;
    }

    function updateContentMinHeight() {
      scope.contentStyle['box-sizing'] = 'border-box';
      scope.contentStyle['min-height'] = (scope.heights.app - scope.heights.topDock - scope.heights.bottomDock) + 'px';
    }
  }

});
