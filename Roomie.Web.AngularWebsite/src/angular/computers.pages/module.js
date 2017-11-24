import computersModule from '../computers/module.js';
import defineModule from '../defineModule.js';
import ComputerDetailController from './ComputerDetailController/index.js';
import ComputerListController from './ComputerListController/index.js';
import routing from './routing.js';

let module = defineModule({
  name: 'roomie.computers.pages', 
  dependencies: [
    computersModule,
    'ui.router',
  ],
  definitions: [
    ComputerDetailController,
    ComputerListController,
  ],
  config: [
    routing,
  ],
});

export default module;
