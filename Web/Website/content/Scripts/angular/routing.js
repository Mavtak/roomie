var module = angular.module('roomie.app');

module.run(['$rootScope', '$state', function ($rootScope, $state) {
  $rootScope.$state = $state;
}]);

module.config(['$stateProvider', function ($stateProvider) {
}]);
