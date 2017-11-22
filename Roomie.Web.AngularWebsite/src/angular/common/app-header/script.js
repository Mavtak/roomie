import template from './template.html';

function appHeader(
  pageMenuItems
) {

  return {
    restrict: 'E',
    scope: {
      navigationMenu: '=navigationMenu',
      pageMenu: '=pageMenu',
    },
    link: link,
    template: template,
  };

  function link(scope) {
    scope.pageMenuItems = pageMenuItems;
  }

};

export default appHeader;
