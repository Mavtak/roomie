import tasksModule from '../tasks/module.js';
import setUpModule from '../setUpModule.js';
import TasksController from './TasksController/index.js';
import routing from './routing.js';

let module = angular.module('roomie.tasks.pages', [
  tasksModule.name,
  'ui.router',
]);

setUpModule(module, [
  TasksController,
]);

module.config(routing);

export default module;
