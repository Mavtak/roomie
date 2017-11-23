import template from './template.html';

function roomieBot() {

  return {
    restrict: 'E',
    scope: {
      mood: '=mood',
    },
    template: template,
  };

}

export default roomieBot;
