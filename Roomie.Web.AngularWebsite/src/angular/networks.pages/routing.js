import detailTemplate from './detail.html';
import indexTemplate from './index.html';

function routing(
  $stateProvider
) {

  $stateProvider.state('network list', {
    url: '/networks',
    controller: 'NetworkListController',
    controllerAs: 'controller',
    template: indexTemplate,
  });

  $stateProvider.state('network detail', {
    url: '/networks/:id',
    controller: 'NetworkDetailController',
    controllerAs: 'controller',
    template: detailTemplate,
  });

}

export default routing;
