import commonModule from '../common/module.js';
import setUpModule from '../setUpModule.js';
import addComputerWidget from './add-computer-widget/index.js';
import computerWidget from './computer-widget/index.js';
import runScriptControls from './run-script-controls/index.js';
import webHookControls from './web-hook-controls/index.js';

let module = angular.module('roomie.computers', [
  commonModule.name,
]);

setUpModule(module, [
  addComputerWidget,
  computerWidget,
  runScriptControls,
  webHookControls,
]);

export default module;
