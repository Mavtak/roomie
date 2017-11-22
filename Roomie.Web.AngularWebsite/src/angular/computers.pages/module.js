import computersModule from '../computers/module.js';
import setUpModule from '../setUpModule.js';
import ComputerDetailController from './ComputerDetailController/index.js';
import ComputerListController from './ComputerListController/index.js';
import routing from './routing.js';

let module = angular.module('roomie.computers.pages', [
  computersModule.name,
  'ui.router',
]);

setUpModule(module, [
  ComputerDetailController,
  ComputerListController,
]);

module.config(routing);

export default module;
