import commonModule from '../common/module.js';
import defineModule from '../defineModule.js';
import networkEditControls from './network-edit-controls/index.js';
import networkWidget from './network-widget/index.js';


let module = defineModule({
  name: 'roomie.networks',
  dependencies: [
    commonModule,
  ],
  definitions: [
    networkEditControls,
    networkWidget,
  ],
});

export default module;
