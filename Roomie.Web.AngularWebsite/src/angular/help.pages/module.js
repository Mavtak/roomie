import commonModule from '../common/module.js';
import defineModule from '../defineModule.js';
import CommandDocumentationController from './CommandDocumentationController/index.js';
import routing from './routing.js';

let module = defineModule({
  name: 'roomie.help.pages',
  dependencies: [
    commonModule,
  ],
  definitions: [
    CommandDocumentationController,
  ],
  config: [
    routing,
  ],
});

export default module;
