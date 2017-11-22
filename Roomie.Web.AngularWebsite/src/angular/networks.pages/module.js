import networksModule from '../networks/module.js';
import setUpModule from '../setUpModule.js';
import NetworkDetailController from './NetworkDetailController/index.js';
import NetworkListController from './NetworkListController/index.js';
import routing from './routing.js';

let module = angular.module('roomie.networks.pages', [
  networksModule.name,
  'ui.router',
]);

setUpModule(module, [
  NetworkDetailController,
  NetworkListController,
]);

module.config(routing);

export default module;
