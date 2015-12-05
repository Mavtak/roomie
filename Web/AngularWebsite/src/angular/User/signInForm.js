var module = angular.module('roomie.users');

module.directive('signInForm', function() {

  return {
    restrict: 'E',
    scope: {
      password: '=password',
      username: '=username',
      submit: '=submit'
    },
    template: '' +
      '<div>' +
        '<div>username: <input type="text" ng-model="username"></div>' +
        '<div>password: <input type="password" ng-model="password"></div>' +
        '<button ng-click="submit()">Submit</button>' +
      '</div>'
  };

});
