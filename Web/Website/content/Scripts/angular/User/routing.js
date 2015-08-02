﻿var module = angular.module('roomie.users');

module.config(['$stateProvider', function($stateProvider) {
  $stateProvider.state('sign-in', {
    url: '/sign-in',
    controller: 'SignInController',
    template: '' +
      '<sign-in-form ' +
        'username="username" ' +
        'password="password" ' +
        'submit="submit"' +
        '>' +
      '</sign-in-form>'
  });
}]);