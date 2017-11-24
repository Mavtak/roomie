import defineModule from '../defineModule.js';
import signInForm from './sign-in-form/index.js';

let module = defineModule({
  name: 'roomie.users', 
  definitions: [
    signInForm,
  ],
});

export default module;
