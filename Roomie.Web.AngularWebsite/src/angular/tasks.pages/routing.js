import indexTemplate from './index.html';

function routing(
  $stateProvider
) {

  $stateProvider.state('tasks', {
    url: '/tasks?start&count',
    controller: 'TasksController',
    controllerAs: 'controller',
    template: indexTemplate,
  });

}

export default routing;
