﻿import template from './template.html';

function sideMenu(
  $window
) {

  return {
    restrict: 'E',
    transclude: true,
    scope: {
      calculatedWidth: '=calculatedWidth',
      side: '@side'
    },
    link: link,
    template: template,
  };

  function link(scope, element, attributes) {
    scope.style = {
      width: 'inherit',
      position: 'absolute',
      top: 0,
      bottom: 0
    };

    if (attributes.side === 'left') {
      scope.style.left = 0;
    }

    if (attributes.side === 'right') {
      scope.style.right = 0;
    }

    if (attributes.hasOwnProperty('calculatedWidth')) {
      scope.$watch(calculateWidth, updateWidth);
    }

    function calculateWidth() {
      var sideMenu = element.contents()[0];
      var result = sideMenu.offsetWidth;

      if (result === 0 && typeof scope.calculatedWidth !== 'undefined') {
        result = scope.calculatedWidth;
      } else {
        result += 'px';
      }

      return result;
    }

    function updateWidth(newValue) {
      scope.calculatedWidth = newValue;
    }
  }

}

export default sideMenu;
