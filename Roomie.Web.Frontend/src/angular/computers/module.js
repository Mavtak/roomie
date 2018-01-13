import commonModule from '../common/module.js';
import defineModule from '../defineModule.js';
import addComputerWidget from './add-computer-widget/index.js';
import computerWidget from './computer-widget/index.js';
import runScriptControls from './run-script-controls/index.js';
import webHookControls from './web-hook-controls/index.js';

let module = defineModule({
  name: 'roomie.computers',
  dependencies: [
    commonModule,
  ],
  definitions: [
    addComputerWidget,
    computerWidget,
    runScriptControls,
    webHookControls,
  ],
});

export default module;
