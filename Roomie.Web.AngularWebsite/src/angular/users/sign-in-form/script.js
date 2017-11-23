import template from './template.html';

function signInForm() {

  return {
    restrict: 'E',
    scope: {
      password: '=password',
      username: '=username',
      submit: '=submit'
    },
    template: template,
  };

}

export default signInForm;
