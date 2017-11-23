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
import defaultRouting from './defaultRouting.js';
import notFoundROuting from './notFoundRouting.js';

let module = angular.module('roomie.app', [
  computersModule.name,
  computersPagesModule.name,
  devicesModule.name,
  devicesPagesModule.name,
  helpPagesModule.name,
  networksModule.name,
  networksPagesModule.name,
  tasksModule.name,
  tasksPagesModule.name,
  userModule.name,
  usersPagesModule.name,
]);

module.config(defaultRouting);
module.config(notFoundROuting);

export default module;
