import commonModule from '../common/module.js';
import defineModule from '../defineModule.js';
import received from './received/index.js';
import taskWidget from './task-widget/index.js';

let module = defineModule({
  name: 'roomie.tasks',
  dependencies: [
    commonModule,
  ],
  definitions: [
    received,
    taskWidget,
  ],
});

export default module;
