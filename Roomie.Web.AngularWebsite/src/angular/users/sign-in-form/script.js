function signInForm() {

  return {
    restrict: 'E',
    scope: {
      password: '=password',
      username: '=username',
      submit: '=submit'
    },
    templateUrl: 'users/sign-in-form/template.html',
  };

}

export default signInForm;
