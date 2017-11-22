import template from './template.html';

function sideMenuButton() {

  return {
    restrict: 'E',
    scope: {
      'close': '&close',
      'isOpen': '=isOpen',
      'open': '&open'
    },
    link: link,
    template: template,
  };

  function link(scope) {
    scope.isOpen = false;
    scope.toggle = toggle;

    function toggle() {
      var action = scope.isOpen ? scope.close : scope.open;
      scope.isOpen = !scope.isOpen;

      if (typeof action === 'function') {
        action();
      }
    }
  }

}

export default sideMenuButton;
