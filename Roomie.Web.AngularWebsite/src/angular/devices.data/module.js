import dataModuleModule from '../data/module.js';
import devicesModuleModule from '../devices/module.js';
import defineModule from '../defineModule.js';
import AutomaticPollingDeviceListing from './AutomaticPollingDeviceListing/index.js';
import devices from './devices/index.js';
import deviceUtilities from './deviceUtilities/index.js';
import locationUtilities from './locationUtilities/index.js';

let module = defineModule({
  name: 'roomie.devices.data',
  dependencies: [
    dataModuleModule,
    devicesModuleModule,
  ],
  definitions: [
    AutomaticPollingDeviceListing,
    devices,
    deviceUtilities,
    locationUtilities,
  ],
});

export default module;
