import commonModule from '../common/module.js';
import setUpModule from '../setUpModule.js';
import received from './received/index.js';
import taskWidget from './task-widget/index.js';

let module = angular.module('roomie.tasks', [
  commonModule.name,
]);

setUpModule(module, [
  received,
  taskWidget,
]);

export default module;
