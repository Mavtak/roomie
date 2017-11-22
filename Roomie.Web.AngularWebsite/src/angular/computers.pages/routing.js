function routing(
  $stateProvider
) {

  $stateProvider.state('computer list', {
    url: '/computers',
    controller: 'ComputerListController',
    controllerAs: 'controller',
    templateUrl: 'computers.pages/index.html',
  });

  $stateProvider.state('computer detail', {
    url: '/computers/:id',
    controller: 'ComputerDetailController',
    controllerAs: 'controller',
    templateUrl: 'computers.pages/detail.html',
  });

}

export default routing;
