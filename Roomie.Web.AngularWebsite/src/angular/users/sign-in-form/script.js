angular.module('roomie.users').directive('signInForm', function () {

  return {
    restrict: 'E',
    scope: {
      password: '=password',
      username: '=username',
      submit: '=submit'
    },
    templateUrl: 'users/sign-in-form/template.html',
  };

});
