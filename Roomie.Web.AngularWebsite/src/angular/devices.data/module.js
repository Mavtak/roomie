import dataModuleModule from '../data/module.js';
import devicesModuleModule from '../devices/module.js';
import setUpModule from '../setUpModule.js';
import AutomaticPollingDeviceListing from './AutomaticPollingDeviceListing/index.js';
import devices from './devices/index.js';
import deviceUtilities from './deviceUtilities/index.js';
import locationUtilities from './locationUtilities/index.js';

let module = angular.module('roomie.devices.data', [
  dataModuleModule.name,
  devicesModuleModule.name,
]);

setUpModule(module, [
  AutomaticPollingDeviceListing,
  devices,
  deviceUtilities,
  locationUtilities,
]);

export default module;
