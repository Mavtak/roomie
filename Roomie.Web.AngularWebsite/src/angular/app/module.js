import computersModule from '../computers/module.js';
import computersPagesModule from '../computers.pages/module.js';
import devicesModule from '../devices/module.js';
import devicesPagesModule from '../devices.pages/module.js';
import helpPagesModule from '../help.pages/module.js';
import networksModule from '../networks/module.js';
import networksPagesModule from '../networks.pages/module.js';
import tasksModule from '../tasks/module.js';
import tasksPagesModule from '../tasks.pages/module.js';
import userModule from '../users/module.js';
import usersPagesModule from '../users.pages/module.js';
import defineModule from '../defineModule.js';
import defaultRouting from './defaultRouting.js';
import notFoundROuting from './notFoundRouting.js';

let module = defineModule({
  name: 'roomie.app',
  config: [
    defaultRouting,
    notFoundROuting,
  ],
  dependencies: [
    computersModule,
    computersPagesModule,
    devicesModule,
    devicesPagesModule,
    helpPagesModule,
    networksModule,
    networksPagesModule,
    tasksModule,
    tasksPagesModule,
    userModule,
    usersPagesModule,
  ],
});

export default module;
