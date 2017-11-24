import defineModule from '../defineModule.js';
import AutomaticPollingUpdater from './AutomaticPollingUpdater/index.js';
import deviceTypes from './deviceTypes/index.js';
import ManualPoller from './ManualPoller/index.js';
import ManualPollingUpdater from './ManualPollingUpdater/index.js';
import ManualUpdater from './ManualUpdater/index.js';

let module = defineModule({
  name: 'roomie.data',
  definitions: [
    AutomaticPollingUpdater,
    deviceTypes,
    ManualPoller,
    ManualPollingUpdater,
    ManualUpdater,
  ],
});

export default module;
