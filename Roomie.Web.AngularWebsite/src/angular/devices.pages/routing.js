import detailTemplate from './detail.html';
import indexTemplate from './index.html';

function routing(
  $stateProvider
) {

  $stateProvider.state('devices', {
    url: '/devices?examples&location',
    controller: 'DevicesController',
    controllerAs: 'controller',
    template: indexTemplate,
  });

  $stateProvider.state('device detail', {
    url: '/devices/:id',
    controller: 'DevicesController',
    controllerAs: 'controller',
    template: detailTemplate,
  });

}

export default routing;
