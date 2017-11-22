function routing(
  $stateProvider
) {

  $stateProvider.state('tasks', {
    url: '/tasks?start&count',
    controller: 'TasksController',
    controllerAs: 'controller',
    templateUrl: 'tasks.pages/index.html',
  });

}

export default routing;
