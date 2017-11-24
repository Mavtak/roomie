import tasksModule from '../tasks/module.js';
import defineModule from '../defineModule.js';
import TasksController from './TasksController/index.js';
import routing from './routing.js';

let module = defineModule({
  name: 'roomie.tasks.pages',
  dependencies: [
    tasksModule,
    'ui.router',
  ],
  definitions: [
    TasksController,
  ],
  config: [
    routing,
  ]
});

export default module;
