import template from './template.html';

function widgetButton() {

  return {
    restrict: 'E',
    scope: {
      activate: '&activate',
      activated: '=activated',
      label: "@label",
      color: "@color"
    },
    link: link,
    template: template,
  };

  function link(scope) {
    scope.style = {};

    updateColor();

    scope.$watch('color', updateColor);

    function updateColor() {
      scope.style['background-color'] = scope.color;
    }
  }

};

export default widgetButton;
