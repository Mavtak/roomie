import networksModule from '../networks/module.js';
import defineModule from '../defineModule.js';
import NetworkDetailController from './NetworkDetailController/index.js';
import NetworkListController from './NetworkListController/index.js';
import routing from './routing.js';

let module = defineModule({
  name: 'roomie.networks.pages',
  dependencies: [
    networksModule,
    'ui.router',
  ],
  definitions: [
    NetworkDetailController,
    NetworkListController,
  ],
  config: [
    routing,
  ],
});

export default module;
