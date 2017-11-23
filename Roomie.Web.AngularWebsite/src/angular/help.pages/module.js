import commonModule from '../common/module.js';
import setUpModule from '../setUpModule.js';
import CommandDocumentationController from './CommandDocumentationController/index.js';
import routing from './routing.js';

let module = angular.module('roomie.help.pages', [
  commonModule.name,
]);

setUpModule(module, [
  CommandDocumentationController,
]);

module.config(routing);

export default module;
