import template from './template.html';

function appContentLoadingIndicator(
  wholePageStatus
) {

  return {
    restrict: 'E',
    link: link,
    template: template,
  };

  function link(scope) {
    scope.wholePageStatus = wholePageStatus;
  }

}

export default appContentLoadingIndicator;
