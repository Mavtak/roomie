import SignInController from './SignInController/index.js';
import SignOutController from './SignOutController/index.js';
import routing from './routing.js';

let module = angular.module('roomie.users.pages', [
  'ui.router',
]);

[
  SignInController,
  SignOutController,
].forEach(x => module[x.type](x.name, x.value));

module.config(routing);

export default module;
