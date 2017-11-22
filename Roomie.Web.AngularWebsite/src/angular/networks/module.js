import commonModule from '../common/module.js';
import setUpModule from '../setUpModule.js';
import networkEditControls from './network-edit-controls/index.js';
import networkWidget from './network-widget/index.js';


let module = angular.module('roomie.networks', [
  commonModule.name,
]);

setUpModule(module, [
  networkEditControls,
  networkWidget,
]);

export default module;
