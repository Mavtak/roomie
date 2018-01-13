import detailTemplate from './detail.html';
import indexTemplate from './index.html';

function routing(
  $stateProvider
) {

  $stateProvider.state('computer list', {
    url: '/computers',
    controller: 'ComputerListController',
    controllerAs: 'controller',
    template: indexTemplate,
  });

  $stateProvider.state('computer detail', {
    url: '/computers/:id',
    controller: 'ComputerDetailController',
    controllerAs: 'controller',
    template: detailTemplate,
  });

}

export default routing;
