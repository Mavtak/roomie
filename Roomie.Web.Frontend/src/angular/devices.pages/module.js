import devicesModule from '../devices/module.js';
import devicesDataModule from '../devices.data/module.js';
import defineModule from '../defineModule.js';
import DevicesController from './DevicesController/index.js';
import routing from './routing.js';

let module = defineModule({
  name: 'roomie.devices.pages',
  dependencies: [
    devicesModule,
    devicesDataModule,
    'ui.router',
  ],
  definitions: [
    DevicesController,
  ],
  config: [
    routing,
  ],
});

export default module;
