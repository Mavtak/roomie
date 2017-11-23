import devicesModule from '../devices/module.js';
import devicesDataModule from '../devices.data/module.js';
import setUpModule from '../setUpModule.js';
import DevicesController from './DevicesController/index.js';
import routing from './routing.js';

let module = angular.module('roomie.devices.pages', [
  devicesModule.name,
  devicesDataModule.name,
  'ui.router',
]);

setUpModule(module, [
  DevicesController,
]);

module.config(routing);

export default module;
