import setUpModule from '../setUpModule.js';
import signInForm from './sign-in-form/index.js';

let module = angular.module('roomie.users', [
]);

setUpModule(module, [
  signInForm,
]);

export default module;
