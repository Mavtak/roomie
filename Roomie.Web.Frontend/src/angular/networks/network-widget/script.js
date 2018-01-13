import template from './template.html';

function networkWidget(
  api
) {

  return {
    restrict: 'E',
    scope: {
      network: '=network',
      showEdit: '=showEdit'
    },
    template: template,
    link: link,
  };

  function link(scope) {
    scope.addDevice = addDevice;
    scope.removeDevice = removeDevice;

    function addDevice() {
      api({
        repository: 'network',
        action: 'addDevice',
        parameters: {
          id: scope.network.id
        }
      });
    }

    function removeDevice() {
      api({
        repository: 'network',
        action: 'removeDevice',
        parameters: {
          id: scope.network.id
        }
      });
    }
  }

}

export default networkWidget;
